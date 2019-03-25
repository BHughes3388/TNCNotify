using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using ADONTEC.Comm;


namespace TNCNotify
{
    public class MYSCOM : TSCom
    {

        public Machine machine;

        public SuperCom.PLSV2.Heidenhain.HN_SYSTEM_PARAMS SysPars;

        public MYSCOM(int CommId)
            : base(CommId, IntPtr.Zero) // pass the window handle to TSCom
        {
            // constructor
        }



        public string MyMakeStr(byte[] b)
        {
            //string s="";
            //SuperCom.CopyByteArrayToString(ref s, b);
            //return s;
            return SuperCom.CopyZeroTerminatedBufferToString(b);
        }

        public bool DoLogIn(string sArea)
        {
            Console.Out.Write("\nLogin " + sArea);

            if (SuperCom.PLSV2.Heidenhain.HN_LogIn(CommId, SuperCom.CopyStringToZeroTerminatedBuffer(sArea)) == 0)
            {
                Console.Out.Write(" OK\n");
                return true;
            }
            else
                Console.Out.Write(" Failed\n");
            return false;
        }

        public bool DoLogOut(string sArea)
        {
            Console.Out.Write("\nLogout " + sArea);

            if (SuperCom.PLSV2.Heidenhain.HN_LogOut(CommId, SuperCom.CopyStringToZeroTerminatedBuffer(sArea)) == 0)
            {
                Console.Out.Write(" OK\n");
                return true;
            }
            else
                Console.Out.Write(" Failed\n");
            return false;
        }

        public int DataGetValue(String sVarPath, ref byte cRetType, ref String sRetValue)
        {
            int res = SuperCom.PLSV2.Heidenhain.HN_DataGetValue(CommId,
                                                                sVarPath, ref cRetType, ref sRetValue);
            return res;
        }


        // AREA INSPECT
        public void GetTNCVersion()
        {
            String sVersionStrings;
            sVersionStrings = "";
            if (SuperCom.PLSV2.Heidenhain.HN_ERR_NONE == SuperCom.PLSV2.Heidenhain.HN_GetTNCVersionEx(CommId, ref sVersionStrings))
            {
                String s;
                Console.Out.WriteLine("Extended Version:");
                // A possible result string: 
                // "TNC-Type=iTNC530;NC-Ver=340494 06 SP7;PLC-Ver=BASIS 54;Option1=%11111111;"

                // The first three values may be the most interested.

                s = SuperCom.GetStrVal(sVersionStrings, "TNC-Type");
                if (s.Length > 0) Console.Out.WriteLine("  TNC-Type: {0}", s);

                s = SuperCom.GetStrVal(sVersionStrings, "NC-Ver");
                if (s.Length > 0) Console.Out.WriteLine("  NC-Ver  : {0}", s);

                s = SuperCom.GetStrVal(sVersionStrings, "PLC-Ver");
                if (s.Length > 0) Console.Out.WriteLine("  PLC-Ver : {0}", s);

                s = SuperCom.GetStrVal(sVersionStrings, "Option1");
                if (s.Length > 0) Console.Out.WriteLine("  Option1 : {0}", s);

                s = SuperCom.GetStrVal(sVersionStrings, "Option2");
                if (s.Length > 0) Console.Out.WriteLine("  Option2 : {0}", s);
            }

        }

        public void getTNCStatus()
        {
            int dwTNCStatus = 0;
            int ret = SuperCom.PLSV2.Heidenhain.HN_GetTNCStatus(CommId, ref dwTNCStatus);

            if (ret == SuperCom.PLSV2.Heidenhain.HN_ERR_NONE)
            {
                if ((dwTNCStatus & SuperCom.PLSV2.Heidenhain.HN_ST_ENABLE_PAL_C) == SuperCom.PLSV2.Heidenhain.HN_ST_ENABLE_PAL_C) // if set
                {
                    Console.Out.WriteLine("critical message");
                }

                Console.Out.WriteLine(" TNC Status = {0}, ret = {1} ", (uint)dwTNCStatus, ret);

            }
            else
            {
                Console.Out.WriteLine("tnc status error");
            }

        }

        public String GetProgramStatus()
        {
            int nErr;
            string strRetBuffer = "";

            // needs AREA_DNC, see description in PDF manual or "Heidenhain_new_Functions.txt"

            nErr = SuperCom.PLSV2.Heidenhain.HN_GetRunInfoEx(CommId,
                                                             SuperCom.PLSV2.Heidenhain.RunInfoRequestType.HN_LSV2_RUNINFO_PROGRAM_STATUS,
                                                             ref strRetBuffer);

            if (nErr == SuperCom.PLSV2.Heidenhain.HN_NO_ERROR)
            {
                //Console.Out.WriteLine(" ProgramStatus={0}", SuperCom.GetStrVal(strRetBuffer, "ProgramStatus"));
                return SuperCom.GetStrVal(strRetBuffer, "ProgramStatus");
            }
                
            else
            {
                //Console.Out.WriteLine("err {0}, {1}", nErr, HNGetLastError());
                return null;
            }

        }

        public string GetExecutionMode()
        {
            int nErr;
            string strRetBuffer = "";

            // needs AREA_DNC, see description in PDF manual or "Heidenhain_new_Functions.txt"

            nErr = SuperCom.PLSV2.Heidenhain.HN_GetRunInfoEx(CommId,
                                                             SuperCom.PLSV2.Heidenhain.RunInfoRequestType.HN_LSV2_RUNINFO_EXECUTION_MODE,
                                                             ref strRetBuffer);


            if (nErr == SuperCom.PLSV2.Heidenhain.HN_NO_ERROR)
            {
                //Console.Out.WriteLine(" ProgramStatus={0}", SuperCom.GetStrVal(strRetBuffer, "ProgramStatus"));
                string executionMode = SuperCom.GetStrVal(strRetBuffer, "ExecutionMode");

                return executionMode;
            }

            else
            {
                Console.Out.WriteLine("MODE err {0}, {1}", nErr, HNGetLastError());
                return null;
            }

        }

        public Dictionary<string, string> GetExecutionPoint()
        {
            int nErr;
            string strRetBuffer = "";

            // needs AREA_DNC, see description in PDF manual or "Heidenhain_new_Functions.txt"

            nErr = SuperCom.PLSV2.Heidenhain.HN_GetRunInfoEx(CommId,
                                                             SuperCom.PLSV2.Heidenhain.RunInfoRequestType.HN_LSV2_RUNINFO_EXECUTION_POINT,
                                                             ref strRetBuffer);

            Dictionary<string, string> execution = new Dictionary<string, string>();

            if (nErr == SuperCom.PLSV2.Heidenhain.HN_NO_ERROR)
            {
                //Console.Out.WriteLine(" ProgramStatus={0}", SuperCom.GetStrVal(strRetBuffer, "ProgramStatus"));
                execution["BlockNr"] = SuperCom.GetStrVal(strRetBuffer, "BlockNr");
                execution["NameSelectedProgram"] = SuperCom.GetStrVal(strRetBuffer, "NameSelectedProgram");
                execution["NameActiveProgram"] = SuperCom.GetStrVal(strRetBuffer, "NameActiveProgram");

                return execution;
            }

            else
            {
                //Console.Out.WriteLine("err {0}, {1}", nErr, HNGetLastError());
                return null;
            }

        }

        public Dictionary<string, string> GetTool()
        {
            int nErr;
            string strRetBuffer = "";

            // needs AREA_DNC, see description in PDF manual or "Heidenhain_new_Functions.txt"

            nErr = SuperCom.PLSV2.Heidenhain.HN_GetRunInfoEx(CommId,
                                                             SuperCom.PLSV2.Heidenhain.RunInfoRequestType.HN_LSV2_RUNINFO_TOOL,
                                                             ref strRetBuffer);

            Dictionary<string, string> tool = new Dictionary<string, string>();

            if (nErr == SuperCom.PLSV2.Heidenhain.HN_NO_ERROR)
            {
                //Console.Out.WriteLine(" ProgramStatus={0}", SuperCom.GetStrVal(strRetBuffer, "ProgramStatus"));
                tool["ToolNr"] = SuperCom.GetStrVal(strRetBuffer, "ToolNr");
                tool["ToolIndex"] = SuperCom.GetStrVal(strRetBuffer, "ToolIndex");
                tool["ToolAxis"] = SuperCom.GetStrVal(strRetBuffer, "ToolAxis");
                tool["ToolLen"] = SuperCom.GetStrVal(strRetBuffer, "ToolLen");
                tool["ToolRad"] = SuperCom.GetStrVal(strRetBuffer, "ToolRad");

                return tool;
            }

            else
            {
                //Console.Out.WriteLine("err {0}, {1}", nErr, HNGetLastError());
                return null;
            }

        }

        public String GetNCUpTime()
        {
            int nErr;
            string strRetBuffer = "";

            // needs AREA_DNC, see description in PDF manual or "Heidenhain_new_Functions.txt"

            nErr = SuperCom.PLSV2.Heidenhain.HN_GetRunInfoEx(CommId,
                                                             SuperCom.PLSV2.Heidenhain.RunInfoRequestType.HN_LSV2_RUNINFO_NC_UPTIME,
                                                             ref strRetBuffer);

            if (nErr == SuperCom.PLSV2.Heidenhain.HN_NO_ERROR)
            {
                //Console.Out.WriteLine(" ProgramStatus={0}", SuperCom.GetStrVal(strRetBuffer, "ProgramStatus"));
                return SuperCom.GetStrVal(strRetBuffer, "NC-Uptime");
            }

            else
            {
                //Console.Out.WriteLine("err {0}, {1}", nErr, HNGetLastError());
                return null;
            }

        }

        public String GetPositionControlCycleTime()
        {
            int nErr;
            string strRetBuffer = "";

            // needs AREA_DNC, see description in PDF manual or "Heidenhain_new_Functions.txt"

            nErr = SuperCom.PLSV2.Heidenhain.HN_GetRunInfoEx(CommId,
                                                             SuperCom.PLSV2.Heidenhain.RunInfoRequestType.HN_LSV2_RUNINFO_PCCT,
                                                             ref strRetBuffer);

            if (nErr == SuperCom.PLSV2.Heidenhain.HN_NO_ERROR)
            {
                Console.Out.WriteLine(" ProgramStatus={0}", SuperCom.GetStrVal(strRetBuffer, "ProgramStatus"));
                return SuperCom.GetStrVal(strRetBuffer, "Position-Control-Cycle-Time");
            }

            else
            {
                Console.Out.WriteLine("err {0}, {1}", nErr, HNGetLastError());
                return null;
            }

        }

        public void TestGetMC(String strRequest)
        {
            int nErr;

            // requires access rights to AREA_INSPECT

            Console.Out.WriteLine("");
            Console.Out.WriteLine(" Get Machine Parameters   ");

            String strParamStringValues;
            strParamStringValues = "";

            nErr = SuperCom.PLSV2.Heidenhain.HN_GetMachineParameters(CommId, strRequest, ref strParamStringValues);
            if (nErr == SuperCom.PLSV2.Heidenhain.HN_NO_ERROR)
                Console.Out.WriteLine(" OK, [{0}], [{1}]", strRequest, strParamStringValues);
            else
                Console.Out.WriteLine(" failed Err={0}, TNC ErrCode={1}", nErr, HNGetLastError());

            Console.Out.WriteLine("");
        }

        public String HNGetLastError()
        {
            short wErrorLen;
            byte[] szErrorText = new byte[128];
            szErrorText[0] = 0;
            wErrorLen = 0;

            SuperCom.PLSV2.Heidenhain.HN_GetLastError(CommId, szErrorText, ref wErrorLen);
            return SuperCom.CopyZeroTerminatedBufferToString(szErrorText);
        }

        public Error GetFirstError()
        {
            int nErr;
            string strRetBuffer = "";

            // needs AREA_DNC, see description in PDF manual or "Heidenhain_new_Functions.txt"

            nErr = SuperCom.PLSV2.Heidenhain.HN_GetRunInfoEx(CommId,
                                                             SuperCom.PLSV2.Heidenhain.RunInfoRequestType.HN_LSV2_RUNINFO_FIRST_ERROR,
                                                             ref strRetBuffer);

            if (nErr == SuperCom.PLSV2.Heidenhain.HN_NO_ERROR)
            {
                Error FirstError = new Error();

                string ErrorText = SuperCom.GetStrVal(strRetBuffer, "ErrorText");
                if (ErrorText.Length > 0) FirstError.Error_Text = ErrorText;

                string ErrorClass = SuperCom.GetStrVal(strRetBuffer, "ErrorClass");
                if (ErrorClass.Length > 0) FirstError.Error_Class = ErrorClass;

                string ErrorGroup = SuperCom.GetStrVal(strRetBuffer, "ErrorGroup");
                if (ErrorGroup.Length > 0) FirstError.Error_Group = ErrorGroup;

                string ErrorNumber = SuperCom.GetStrVal(strRetBuffer, "ErrorNumber");
                if (ErrorNumber.Length > 0) FirstError.Error_Nr = ErrorNumber;

                //Console.WriteLine("First Error: " + FirstError.Error_Text);

                return FirstError;
            }
            else
            {
                //Console.Out.WriteLine("err {0}, {1}", nErr, HNGetLastError());

                return null;
            }

        }

        public Error GetNextError()
        {
            int nErr;
            string strRetBuffer = "";

            // needs AREA_DNC, see description in PDF manual or "Heidenhain_new_Functions.txt"

            nErr = SuperCom.PLSV2.Heidenhain.HN_GetRunInfoEx(CommId,
                                                             SuperCom.PLSV2.Heidenhain.RunInfoRequestType.HN_LSV2_RUNINFO_NEXT_ERROR,
                                                             ref strRetBuffer);

            if (nErr == SuperCom.PLSV2.Heidenhain.HN_NO_ERROR)
            {
                Console.WriteLine("Com id: " + CommId);

                Error FirstError = new Error();

                string ErrorText = SuperCom.GetStrVal(strRetBuffer, "ErrorText");
                if (ErrorText.Length > 0) FirstError.Error_Text = ErrorText;

                string ErrorClass = SuperCom.GetStrVal(strRetBuffer, "ErrorClass");
                if (ErrorClass.Length > 0) FirstError.Error_Class = ErrorClass;

                string ErrorGroup = SuperCom.GetStrVal(strRetBuffer, "ErrorGroup");
                if (ErrorGroup.Length > 0) FirstError.Error_Group = ErrorGroup;

                string ErrorNumber = SuperCom.GetStrVal(strRetBuffer, "ErrorNumber");
                if (ErrorNumber.Length > 0) FirstError.Error_Nr = ErrorNumber;

                Console.WriteLine("Next Error: " + FirstError.Error_Text);

                return FirstError;
            }
            else
            {
                Console.Out.WriteLine("err {0}, {1}", nErr, HNGetLastError());

                return null;
            }

        }

    }

    public class MachineCom
    {
        public Machine machine;

        private System.Timers.Timer aTimer;

        private bool errorReset;

        public void StartMachine(Machine myMachine, int index)
        {
            machine = myMachine;
            CreateConnection(index, myMachine.IP, "19000");
        }

        public void CreateConnection(int Com, string ip, string port)
        {

            MYSCOM SCom = new MYSCOM(Com); //<<< Set COM channel here <<<

            SCom.ComType = ComType.COMTYPE_WINSOCK_CLIENT;
            SCom.PortOpen = true;

            SCom.ConnectAddress = ip +":" + port;
            SCom.ConnectTimeout = SuperCom.SEC_10;

            SCom.Connect = true;

            // lazy busy wait. It is better to let the events run and enable buttons, menus etc.
            while (SCom.JobProcessing == true)
            {
                SuperCom.RS_Delay(10);
            }

            if (SCom.Connected == false)
            {
                Console.Out.WriteLine("Server not found.");
                Console.ReadLine();
                return;
            }

            // Config LSV2
            SuperCom.PLSV2.RS_SetBCCType(SCom.CommId, SuperCom.PLSV2.BCC_HEIDENHAIN, 0);

            // Config LSV2 Heidenhain
            SuperCom.PLSV2.Heidenhain.HN_GetSetMasterFlag(SCom.CommId, SuperCom.IS_TRUE);

            // Start
            // if HN_SetConfig is also setting SET_BUF, then the privilege to INSPECT is required!
            SCom.DoLogIn(SuperCom.PLSV2.Heidenhain.AREA_INSPECT);

            byte[] szHNConfig = new byte[128];
            SuperCom.CopyStringToByteArray(ref szHNConfig, "IsMASTER=1;IsML=1;SetBuf=255;TOReply=1000;");
            SuperCom.PLSV2.Heidenhain.HN_SetConfig(SCom.CommId, szHNConfig);

            // At some early point bevor it is used to show version or read memory addresses
            SCom.SysPars = new SuperCom.PLSV2.Heidenhain.HN_SYSTEM_PARAMS(SCom.CommId); // needs area INSPECT

            SCom.DoLogIn(SuperCom.PLSV2.Heidenhain.AREA_DATA);

            errorReset = true;
            SetTimer(SCom);
            Console.WriteLine("started connection");

            //SCom.GetTNCVersion();
            //SCom.getTNCStatus();
            //Console.WriteLine("programStatus: {0}", programStatus);

        }

        private void SetTimer(MYSCOM SCom)
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(60000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += (sender, e) => OnTimedEvent(sender, e, SCom);
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

            OnTimedEvent(null, null, SCom);
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e, MYSCOM SCom)
        {


            /*
             * \\PLC\\memory\\S\\10 PGM name
             * \\PLC\\memory\\S\\25 Machine Name
             * \\PLC\\memory\\S\\35 Tool Name
             * \\PLC\\memory\\W\\264 tool number
             * \\PLC\\memory\\W\\492 percentage spindle override NC to PLC
             * \\PLC\\memory\\W\\494 percentage feed override NC to PLC
             * \\PLC\\memory\\W\\764 percentage spindle override PLC to NC
             * \\PLC\\memory\\W\\766 percentage feed override PLC to NC
             * \\TABLE\\'TNC:\\TNCNotify.TAB'\\NR\\0\\PAL Current Pallet Number
             * \\PLC\\memory\\M\\4227 PLC error operand 
             */
            
            //Dictionary To Hold New Machine Data
            Dictionary<string, string> machineDict = new Dictionary<string, string>();

            //Program Status
            string programStatus = SCom.GetProgramStatus();
            Console.WriteLine("programStatus: {0}", programStatus);
            machineDict["ProgramStatus"] = programStatus;


            
            //Program Execution Point
            Dictionary<string, string> executionPoint = SCom.GetExecutionPoint();
            machineDict["NameSelectedProgram"] = executionPoint["NameSelectedProgram"];
            machineDict["NameActiveProgram"] = executionPoint["NameActiveProgram"];
            machineDict["BlockNr"] = executionPoint["BlockNr"];

            //Execution Mode
            string executionMode = SCom.GetExecutionMode();
            machineDict["ExecutionMode"] = executionMode;
            
            //Tool
            Dictionary<string, string> tool = SCom.GetTool();
            machineDict["ToolNr"] = tool["ToolNr"];
            machineDict["ToolIndex"] = tool["ToolIndex"];
            machineDict["ToolAxis"] = tool["ToolAxis"];
            machineDict["ToolLen"] = tool["ToolLen"];
            machineDict["ToolRad"] = tool["ToolRad"];

            Dictionary<string, string>[] matrix = new Dictionary<string, string>[]
            {
                new Dictionary<string, string>(),
                new Dictionary<string, string>(),
                new Dictionary<string, string>(),
                new Dictionary<string, string>(),
                new Dictionary<string, string>(),
                new Dictionary<string, string>(),
                new Dictionary<string, string>(),
                new Dictionary<string, string>(),
                new Dictionary<string, string>(),
                new Dictionary<string, string>(),
                new Dictionary<string, string>(),
                new Dictionary<string, string>(),
                new Dictionary<string, string>(),
                new Dictionary<string, string>(),
                new Dictionary<string, string>(),
                //new Dictionary<string, string>()
            };

            //Tool Name
            string toolNr = tool["ToolNr"];
            matrix[0].Add("Path", $"\\TABLE\\TOOL\\T\\{toolNr}\\NAME");
            matrix[0].Add("Key", "ToolName");
            //Tool Length
            matrix[1].Add("Path", $"\\TABLE\\TOOL\\T\\{toolNr}\\L");
            matrix[1].Add("Key", "ToolLen");
            //Tool Radius
            matrix[2].Add("Path", $"\\TABLE\\TOOL\\T\\{toolNr}\\R");
            matrix[2].Add("Key", "ToolRad");
            //Tool Radius2
            matrix[3].Add("Path", $"\\TABLE\\TOOL\\T\\{toolNr}\\R2");
            matrix[3].Add("Key", "ToolRad2");
            //Tool length Oversize
            matrix[4].Add("Path", $"\\TABLE\\TOOL\\T\\{toolNr}\\DL");
            matrix[4].Add("Key", "ToolLenOversize");
            //Tool Radius Oversize
            matrix[5].Add("Path", $"\\TABLE\\TOOL\\T\\{toolNr}\\DR");
            matrix[5].Add("Key", "ToolRadOversize");
            //Tool Replacement Tool
            matrix[6].Add("Path", $"\\TABLE\\TOOL\\T\\{toolNr}\\RT");
            matrix[6].Add("Key", "ToolReplacmentToolNr");
            //Tool Time2
            matrix[7].Add("Path", $"\\TABLE\\TOOL\\T\\{toolNr}\\TIME2");
            matrix[7].Add("Key", "ToolTime2");
            //Tool Current Time
            matrix[8].Add("Path", $"\\TABLE\\TOOL\\T\\{toolNr}\\CUR.TIME");
            matrix[8].Add("Key", "ToolCurTime");
            //Machine Name
            matrix[9].Add("Path", "\\PLC\\memory\\S\\25");
            matrix[9].Add("Key", "MachineName");
            //Pallet Number
            //matrix[10].Add("Path", "\\PLC\\memory\\S\\2");
            //matrix[10].Add("Key", "PalletNr");
            //NC Spindle Override
            matrix[10].Add("Path", "\\PLC\\memory\\W\\492");
            matrix[10].Add("Key", "NCSpindleOverride");
            //NC Feed Override
            matrix[11].Add("Path", "\\PLC\\memory\\W\\494");
            matrix[11].Add("Key", "NCFeedOverride");
            //Actual Spindle Override
            matrix[12].Add("Path", "\\PLC\\memory\\W\\764");
            matrix[12].Add("Key", "PLCSpindleOverride");
            //Actual Feed Override
            matrix[13].Add("Path", "\\PLC\\memory\\W\\766");
            matrix[13].Add("Key", "PLCFeedOverride");
            //Error Operand
            matrix[14].Add("Path", "\\PLC\\memory\\M\\4227");
            matrix[14].Add("Key", "PLCError");

            foreach (Dictionary<string, string> dict in matrix)
            {
                String sRetValue = "";
                byte cType = 32; // space

                if (SuperCom.PLSV2.Heidenhain.HN_ERR_NONE == SCom.DataGetValue(dict["Path"], ref cType, ref sRetValue))
                {
                    machineDict[dict["Key"]] = sRetValue;
                }

            }


            foreach (KeyValuePair<string, string> kvp in machineDict)
            {
                Console.WriteLine("{0} : {1}", kvp.Key, kvp.Value);
            }
            

            //Update Machine On Server
            UpdateMachine(machineDict);

            //Check If Machine Has Error
            CheckForErrors(machineDict["PLCError"], SCom);

            //SCom.DoLogOut(SuperCom.PLSV2.Heidenhain.AREA_DATA);
        }

        private string PalNrFromString(string palletNr)
        {
            int colon = palletNr.IndexOf("Pal:");
            colon = colon + 5;
            palletNr = palletNr.Substring(colon, 2);

            return palletNr;
        }

        private void UpdateMachine(Dictionary<string, string> machineDict)
        {
            //Machine machine = new Machine();

            //machine.machineid = "5c8e7a1ba815dc7dca74d570";
            //machine.MachineName = machineDict["MachineName"];
            machine.NCSpindleOverride = machineDict["NCSpindleOverride"];
            machine.NCFeedOveride = machineDict["NCFeedOverride"];
            machine.PLCSpindleOverride = machineDict["PLCSpindleOverride"];
            machine.PLCFeedOverride = machineDict["PLCFeedOverride"];
            //machine.PAL = PalNrFromString(machineDict["PalletNr"]);
            machine.ProgramStatus = machineDict["ProgramStatus"];
            machine.SelectedProgram = machineDict["NameSelectedProgram"];
            machine.ActiveProgram = machineDict["NameActiveProgram"];
            machine.BlockNr = machineDict["BlockNr"];
            machine.ExecutionMode = machineDict["ExecutionMode"];
            
            machine.ToolName = machineDict["ToolName"];
            machine.ToolNr = machineDict["ToolNr"];
            machine.ToolIndex = machineDict["ToolIndex"];
            machine.ToolAxis = machineDict["ToolAxis"];
            machine.ToolLen = machineDict["ToolLen"];
            machine.ToolRad = machineDict["ToolRad"];
            machine.ToolRad2 = machineDict["ToolRad2"];
            machine.ToolRadOversize = machineDict["ToolRadOversize"];
            machine.ToolLenOversize = machineDict["ToolLenOversize"];
            machine.ToolReplacmentToolNr = machineDict["ToolReplacmentToolNr"];
            machine.ToolTime2 = machineDict["ToolTime2"];
            machine.ToolCurTime = machineDict["ToolCurTime"];

            machine.Connected = true;
            

            Console.WriteLine("Tool Replacement Nr : {0}", machine.machineid);
            if (machine != null)
            {
                KinveyNetworking network = new KinveyNetworking();
                network.UpdateMachine(machine);
            }

        }

        private void CheckForErrors(string PLCError, MYSCOM SCom)
        {
            if (PLCError.Equals("TRUE"))
            {
                Console.WriteLine("4227 Tripped, errorReset: " + errorReset);

                if (errorReset)
                {
                    errorReset = false;
                    GetFirstError(SCom);
                    Console.WriteLine("New Error");
                }
            }
            else
            {
                errorReset = true;
                Console.WriteLine("error reset");
            }
        }

        private void GetFirstError(MYSCOM SCom)
        {
            Error NewError = SCom.GetFirstError();

            if (NewError != null)
            {
                KinveyNetworking network = new KinveyNetworking();
                network.SaveError(NewError, machine);
                GetNextError(SCom);
            }
        }

        private void GetNextError(MYSCOM SCom)
        {
            Error NextError = SCom.GetNextError();

            if (NextError != null)
            {
                KinveyNetworking network = new KinveyNetworking();
                network.SaveError(NextError, machine);
                GetNextError(SCom);
            }
            
        }
    }
}
