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
                return SuperCom.GetStrVal(strRetBuffer, "ProgramStatus");
            }

               return null;
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

        public string GetMachineSerialNumber()
        {
            /*
            Machine serial Nr. : 107.xx.00.xxx
            MP 4210.5  : +107       ;1st position
            MP 4210.6  : +11        ;2nd position
            MP 4210.7  : +0         ;3rd position
            MP 4210.8  : +34        ;4th position
            */

            string serialNumber = "";
            serialNumber += GetMachineParameter("4210.5").Trim(new Char[] { '+' });
            serialNumber += ".";
            serialNumber += GetMachineParameter("4210.6").Trim(new Char[] { '+' });
            serialNumber += ".00.";
            if (GetMachineParameter("4210.8").Length < 4)
            {
                serialNumber += "0";
            }
            serialNumber += GetMachineParameter("4210.8").Trim(new Char[] { '+' });

            return serialNumber;
        }

        public int ReadByteMarker(int dwOfs)
        {
            //needs area PLCDEBUG, 
            // if already used by other instances TNC may refuse access
            // one could try to delay and retry 2x or 3x times in case the other instance releases it
            int nErr;
            int dwPLCAddress = SuperCom.PLSV2.Heidenhain.HN_SwapDWord(SysPars.Values.MarkerStart + dwOfs);
            int dwLen = 1;
            byte[] cBuffer = new byte[dwLen];

            nErr = SuperCom.PLSV2.Heidenhain.HN_ReadMemory(CommId,
                                                           dwPLCAddress,
                                                           cBuffer,
                                                           ref dwLen);

            if (nErr == SuperCom.PLSV2.Heidenhain.HN_NO_ERROR)
            {
                return cBuffer[0];
            }
            else
            {
                Console.Out.WriteLine("err {0}, {1}", nErr, HNGetLastError());
            }

            return 0;
        }

        public long ReadDWord(int dwOfs)
        {
            //needs area PLCDEBUG, if already used by other instances TNC may refuse access
            int nErr;
            int dwPLCAddress = SuperCom.PLSV2.Heidenhain.HN_SwapDWord(SysPars.Values.WordStart + dwOfs);
            int dwLen = 4;
            byte[] cBuffer = new byte[dwLen];

            nErr = SuperCom.PLSV2.Heidenhain.HN_ReadMemory(CommId,
                                                           dwPLCAddress,
                                                           cBuffer,
                                                           ref dwLen);


            if (nErr == SuperCom.PLSV2.Heidenhain.HN_NO_ERROR)
            {
                System.UInt32[] a = ByteArrayToDWordArray(cBuffer, dwLen);

                return SuperCom.PLSV2.Heidenhain.HN_SwapDWord((int)a[0]);
            }
            else
            {
                Console.Out.WriteLine("err {0}, {1}", nErr, HNGetLastError());
            }

            return 0;
        }

        public int ReadWord(int dwOfs)
        {
            //needs area PLCDEBUG, if already used by other instances TNC may refuse access
            int nErr;
            int dwPLCAddress = SuperCom.PLSV2.Heidenhain.HN_SwapDWord(SysPars.Values.WordStart + dwOfs);
            int dwLen = 2;
            byte[] cBuffer = new byte[dwLen];

            nErr = SuperCom.PLSV2.Heidenhain.HN_ReadMemory(CommId,
                                                           dwPLCAddress,
                                                           cBuffer,
                                                           ref dwLen);

            //Console.Out.WriteLine("Read Word at {0}, total bytes {1}, ", dwOfs, dwLen);

            if (nErr == SuperCom.PLSV2.Heidenhain.HN_NO_ERROR)
            {
                
                System.UInt16[] a = ByteArrayToWordArray(cBuffer, dwLen);
                return a[0];
            }
            else
            {
                Console.Out.WriteLine("err {0}, {1}", nErr, HNGetLastError());
            }

            return 0;
        }

        public System.UInt32[] ByteArrayToDWordArray(byte[] aSource, int nByteCount)
        {
            System.UInt32[] aDest = new System.UInt32[nByteCount / 4];
            Buffer.BlockCopy(aSource, 0, aDest, 0, nByteCount);
            return aDest;
        }

        public System.UInt16[] ByteArrayToWordArray(byte[] aSource, int nByteCount)
        {
            System.UInt16[] aDest = new System.UInt16[nByteCount / 2];
            Buffer.BlockCopy(aSource, 0, aDest, 0, nByteCount);
            return aDest;
        }

        public string GetMachineParameter(String strRequest)
        {
            int nErr;

            String strParamStringValues;
            strParamStringValues = "";

            nErr = SuperCom.PLSV2.Heidenhain.HN_GetMachineParameters(CommId, strRequest, ref strParamStringValues);
            string s = SuperCom.GetStrVal(strParamStringValues, strRequest);
            if (nErr == SuperCom.PLSV2.Heidenhain.HN_NO_ERROR)
                return s;//Console.Out.WriteLine(" OK, [{0}], [{1}], {2}", strRequest, strParamStringValues, s);
            else
                return null;//Console.Out.WriteLine(" failed Err={0}, TNC ErrCode={1}", nErr, HNGetLastError());

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
                if (ErrorText.Length > 0) FirstError.ErrorText = ErrorText;

                string ErrorClass = SuperCom.GetStrVal(strRetBuffer, "ErrorClass");
                if (ErrorClass.Length > 0) FirstError.ErrorClass = ErrorClass;

                string ErrorGroup = SuperCom.GetStrVal(strRetBuffer, "ErrorGroup");
                if (ErrorGroup.Length > 0) FirstError.ErrorGroup = ErrorGroup;

                string ErrorNumber = SuperCom.GetStrVal(strRetBuffer, "ErrorNumber");
                if (ErrorNumber.Length > 0) FirstError.ErrorNumber = ErrorNumber;

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
                //Console.WriteLine("Com id: " + CommId);

                Error FirstError = new Error();

                string ErrorText = SuperCom.GetStrVal(strRetBuffer, "ErrorText");
                if (ErrorText.Length > 0) FirstError.ErrorText = ErrorText;

                string ErrorClass = SuperCom.GetStrVal(strRetBuffer, "ErrorClass");
                if (ErrorClass.Length > 0) FirstError.ErrorClass = ErrorClass;

                string ErrorGroup = SuperCom.GetStrVal(strRetBuffer, "ErrorGroup");
                if (ErrorGroup.Length > 0) FirstError.ErrorGroup = ErrorGroup;

                string ErrorNumber = SuperCom.GetStrVal(strRetBuffer, "ErrorNumber");
                if (ErrorNumber.Length > 0) FirstError.ErrorNumber = ErrorNumber;

                Console.WriteLine("Next Error: " + FirstError.ErrorText);

                return FirstError;
            }
            else
            {
                //Console.Out.WriteLine("err {0}, {1}", nErr, HNGetLastError());

                return null;
            }

        }

    }

    public class MachineCom
    {
        public Machine machine;
        public int index;
        public MYSCOM com;

        private System.Timers.Timer aTimer;

        private bool errorReset;

        public void StartMachine(Machine myMachine, int myIndex)
        {
            if (machine == null)
                machine = myMachine;

            if (index <= 0)
                index = myIndex;

            CreateConnection(index, machine.Ip, "19000");
        }

        public void CreateConnection(int Com, string ip, string port)
        {

            MYSCOM SCom = new MYSCOM(Com); //<<< Set COM channel here <<<
            com = SCom;

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

            SCom.DoLogIn(SuperCom.PLSV2.Heidenhain.AREA_PLCDEBUG);

            errorReset = true;
            SetTimer();
            Console.WriteLine("started connection");

        }

        public void closeConnection()
        {
            if (com.Connect)
            {
                com.Connect = false;
                aTimer.Enabled = false;
                com.PortOpen = false;

            }

        }

        private void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(60000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += (sender, e) => UpdateMachine(sender, e);
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

            UpdateMachine(null, null);
        }
       
        private string DataValueForPath(string path)
        {
            String sRetValue = "";
            byte cType = 32; // space

            if (SuperCom.PLSV2.Heidenhain.HN_ERR_NONE == com.DataGetValue(path, ref cType, ref sRetValue))
            {
                return sRetValue;
            }

            return "No Value";
        }

        private async void UpdateMachine(Object source, ElapsedEventArgs e)
        {
            Dictionary<string, string> executionPoint = com.GetExecutionPoint();

            ExecutionData executationData = new ExecutionData()
            {
                ProgramStatus = com.GetProgramStatus(),
                ExecutionMode = com.GetExecutionMode(),
                SelectedProgram = executionPoint["NameSelectedProgram"],
                ActiveProgram = executionPoint["NameActiveProgram"],
                BlockNumber = executionPoint["BlockNr"],
                PalletNumber = string.Format("{0}", com.ReadWord(40))
            };

            Dictionary<string, string> tool = com.GetTool();
            var toolNumber = tool["ToolNr"];

            Tool toolData = new Tool()
            {
                Number = tool["ToolNr"],
                Length = tool["ToolLen"],
                Radius = tool["ToolRad"],
                Name = DataValueForPath($"\\TABLE\\TOOL\\T\\{toolNumber}\\NAME"),
                Radius2 = DataValueForPath($"\\TABLE\\TOOL\\T\\{toolNumber}\\R2"),
                LengthOversize = DataValueForPath($"\\TABLE\\TOOL\\T\\{toolNumber}\\DL"),
                RadiusOversize = DataValueForPath($"\\TABLE\\TOOL\\T\\{toolNumber}\\DR"),
                ReplacmentToolNumber = DataValueForPath($"\\TABLE\\TOOL\\T\\{toolNumber}\\RT"),
                Time = DataValueForPath($"\\TABLE\\TOOL\\T\\{toolNumber}\\TIME2"),
                CurrentTime = DataValueForPath($"\\TABLE\\TOOL\\T\\{toolNumber}\\CUR.TIME")
            };

            machine.ExecutationData = executationData;
            machine.Tool = toolData;

            //Console.WriteLine(com.GetMachineSerialNumber());

            if (machine != null)
            {
                FirestoreNetworking networking = new FirestoreNetworking();
                networking.UpdateMachine(machine);

            }

            GetFirstError();
            
        }
 
        private void CheckForErrors(string PLCError, MYSCOM SCom)
        {
            if (errorReset)
            {
                GetFirstError();
                Console.WriteLine("New Error");
            }

        }

        private void GetFirstError()
        {
            Error NewError = com.GetFirstError();

            if (NewError == null)
            {
                errorReset = true;
            }

            if (NewError != null && errorReset)
            {
                errorReset = false;

                NewError.MachineId = machine.ReferenceId;

                FirestoreNetworking networking = new FirestoreNetworking();
                networking.SaveError(NewError);

                GetNextError();
            }
           
        }

        private void GetNextError()
        {
            Error NextError = com.GetNextError();

            if (NextError != null)
            {
                NextError.MachineId = machine.ReferenceId;

                FirestoreNetworking networking = new FirestoreNetworking();
                networking.SaveError(NextError);

                GetNextError();
            }
            
        }
    }
}

