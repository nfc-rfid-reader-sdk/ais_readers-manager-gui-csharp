using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using HND_AIS = System.UIntPtr;

namespace DL_AIS_Readers
{
    public class Globals
    {
        public bool gui_on = true;
        public HND_AIS[] id_handle = new HND_AIS[100];
        public HND_AIS[] handle = new HND_AIS[100];
        public uint devices = 0;
        public uint[] device_IDs = new uint[100];        
        public bool cmd_finish = false;
        
        public string debug_file_path = "DEBUG_LOG_" + DateTime.Now.ToString("dd-MM-yyyy").Replace("-", "_") + ".txt";

        public string[] settings = new string[7];
        public string gui_command = "0";
        public string str_time_to_send = "";
        public string str_minutes_check = "";
        public string server_address = "";
        public int send_hours = 0;
        public string password = "";
        public int InternetOrComputer = 0;
        public int DaylightSavingTime = 0;        
    }

    public enum DEVICE_TYPES : uint
    {
        DL_UNKNOWN_DEVICE = 0,
        //--------------------
        DL_AIS_100,
        DL_AIS_20,
        DL_AIS_30,
        DL_AIS_35,
        DL_AIS_50,
        DL_AIS_110,
        DL_AIS_LOYALITY,
        DL_AIS_37, // AIS START == BASE BAT USB
                   
        DL_AIS_BMR, // Barcode NFC Reader, Half-Duplex

        DL_AIS_BASE_HD, // Base HD AIS old, Half-Duplex

        DL_AIS_BASE_HD_SDK, // Base HD AIS SDK, Half-Duplex

        DL_AIS_IO_EXTENDER,
    };

    // API card actions:
    public enum CARD_ACTION : uint
    {
        // CARD_FOREIGN
        // strange card - card from different system
        // BASE> LOG = 0x83 | RTE = 0x00
        ACTION_CARD_FOREIGN = 0x00,

        // DISCARDED
        // blocked card - card on blacklist, no valid access right, has no right of passage
        // BASE> LOG= 0xC3 | RTE= 0x20
        ACTION_CARD_DISCARDED = 0x20, // (32 dec)

        // CARD_HACKED
        // Mifare key OK - CRC OK - but bad user data
        // Bad protective data
        // BASE> LOG= 0x84 | RTE= 0x40
        ACTION_CARD_HACKED = 0x40, // (64 dec)

        // CARD_BAD_DATA
        // Mifare key OK - CRC BAD
        // Cards with invalid data - BAD CRC
        // BASE> LOG= 0x-- | RTE= 0x82
        ACTION_CARD_BAD_DATA = 0x50, // (80 dec)

        // CARD_NO_DATA
        // unreadable card - card without or unknown Mifare key
        // BASE> LOG= 0x-- | RTE= 0x81
        ACTION_CARD_NO_DATA = 0x60, // (96 dec)

        // UNLOCKED
        ACTION_QR_UNLOCKED = 0x70,
        ACTION_QR_BLOCKED = 0x71,
        ACTION_QR_UNKNOWN = 0x72,

        // state machines = 0xB000
        ERR_STATE_MACHINE = 0xB000,
        ERR_SM_IDLE__NO_RESPONSE,
        ERR_SM_COMMAND_IN_PROGRESS,
        ERR_SM_NOT_IDLE,
        ERR_SM_CMD_NOT_STARTED,

        // The correct card
        // BASE> LOG= 0xC2 | RTE= 0x80(+++)
        // TWR> 0x80 (128 dec) - A regular passage (P)
        // TWR> 0x90 (144 dec) - Official exit (S)
        // TWR> 0xA0 (160 dec) - Vehicle pass (V)
        // TWR> 0xB0 (176 dec) - Approved exit (O)
        ACTION_CARD_UNLOCKED = 0x80,
        ACTION_CARD_UNLOCKED_1 = 0x81,
        ACTION_CARD_UNLOCKED_2 = 0x82,
        ACTION_CARD_UNLOCKED_3 = 0x83,
        ACTION_CARD_UNLOCKED_4 = 0x84,
        ACTION_CARD_UNLOCKED_5 = 0x85,
        ACTION_CARD_UNLOCKED_6 = 0x86,
        ACTION_CARD_UNLOCKED_7 = 0x87,

        // not used anymore
        //#define CARD_OK			0x85

        // non valid status
        // not used status
        //	ACTION_DEVICE_MISSING = 0xA1,
        //	ACTION_BREAK_THROUGH = 0xA2,
        //	ACTION_DOOR_LEFT_OPEN = 0xA3,

    };

    public enum DL_STATUS : uint
    {
        DL_OK = 0x00,

        //----------------------------------------------------------------------
        // common - mostly used
        TIMEOUT_ERROR,

        NULL_POINTER,
        PARAMETERS_ERROR,
        
        MEMORY_ALLOCATION_ERROR,

        NOT_INITIALIZED,
        ALREADY_INITIALIZED,

        TIMESTAMP_INVALID,

        EVENT_BUSY,

        //----------------------------------------------------------------------
        // specific = 0x1000
        ERR_SPECIFIC = 0x1000,
        // USB RF
        CMD_BRAKE_RTE, // RTE arrived while CMD_IN_PROGRESS

        USB_RF_ACK_FAILED,
        NO_RF_PACKET_DATA,

        TRANSFER_WRITING_ERROR,

        EVENT_WAKEUP_BUSY,

        //----------------------------------------------------------------------
        // resource = 0x2000
        RESOURCE_NOT_ACQUIRED = 0x2000,
        RESOURCE_ALREADY_ACQUIRED,
        RESOURCE_BUSY,

        //----------------------------------------------------------------------
        // FILE = 0x3000
        FILE_OVERSIZE = 0x3000,
        FILE_EMPTY,
        FILE_LOCKED, // when file is fill, and not read yet
        FILE_NOT_FOUND,
        ERR_NO_FILE_NAME,
        ERR_DIR_CAN_NOT_CREATE,

        //----------------------------------------------------------------------
        // LOG = 0x4000
        ERR_DATA = 0x4000,
        ERR_BUFFER_EMPTY,    ///< 0x4001
        ERR_BUFFER_OVERFLOW, ///< 0x4002
        ERR_CHECKSUM, // CRC ERROR
        LOG_NOT_CORRECT,

        //----------------------------------------------------------------------
        //	0x5000,

        //----------------------------------------------------------------------
        //	0x6000,

        //----------------------------------------------------------------------
        // list operations = 0x7000
        LIST_ERROR = 0x7000,
        ITEM_IS_ALREADY_IN_LIST,
        ITEM_NOT_IN_LIST,

        //----------------------------------------------------------------------
        // devices = 0x8000
        NO_DEVICES = 0x8000, // NO_USB_RF_DEVICES
        //----------------------------------------------------------------------
        // open multiple devices
        DEVICE_OPENING_ERROR,
        DEVICE_CAN_NOT_OPEN,
        DEVICE_ALREADY_OPENED,
        DEVICE_NOT_OPENED,
        DEVICE_INDEX_OUT_OF_BOUND,
        DEVICE_CLOSE_ERROR,
        DEVICE_UNKNOWN,

        //----------------------------------------------------------------------
        // command response = 0x9000
        ERR_COMMAND_RESPONSE = 0x9000,
        CMD_RESPONSE_UNKNOWN_COMMAND,
        CMD_RESPONSE_WRONG_CMD,
        CMD_RESPONSE_COMMAND_FAILED,
        CMD_RESPONSE_UNSUCCESS,
        CMD_RESPONSE_NO_AUTHORIZATION,
        CMD_RESPONSE_SIZE_OVERFLOW,
        CMD_RESPONSE_NO_DATA,

        //----------------------------------------------------------------------
        // Threads and objects = 0xA000
        THREAD_FAILURE = 0xA000,
        //---------------------
        ERR_OBJ_NOT_CREATED,
        //---------------------

        //----------------------------------------------------------------------
        // state machines = 0xB000
        ERR_STATE_MACHINE = 0xB000,
        ERR_SM_IDLE__NO_RESPONSE,
        ERR_SM_COMMAND_IN_PROGRESS,
        ERR_SM_NOT_IDLE,

        //----------------------------------------------------------------------
        // readers errors = 0xD000
        READER_ERRORS_ = 0xD000,
        READER_UID_ERROR,
        READER_LOG_ERROR,

        //----------------------------------------------------------------------
        // HAMMING = 0xE000
        DL_HAMMING_ERROR = 0xE000,
        DL_HAMMING_NOT_ACK,
        DL_HAMMING_WRONG_ACK,
        DL_HAMMING_WRONG_REPLAY,

        ERROR_SOME_REPLAY_FALULT,

        //  Formatted transfer
        DL_HAMMING_TERR_TIMEOUT,
        DL_HAMMING_TERR_BAD_FRAME,
        DL_HAMMING_TERR_BAD_SUM,
        DL_HAMMING_TERR_BAD_CODE,
        DL_HAMMING_TERR_TOO_OLD,
        DL_HAMMING_TERR_NOISE, // Warning returned by DecodeFrame()
        DL_HAMMING_TERR_ERROR_MASK,

        //----------------------------------------------------------------------
        // FTDI = 0xF000
        NO_FTDI_COMM_DEVICES = 0xF000,
        NO_FTDI_COMM_DEVICES_OPENED,

        ERR_FTDI, // global
        ERR_FTDI_READ,
        ERR_FTDI_READ_LESS_DATA,
        ERR_FTDI_WRITE,
        ERR_FTDI_WRITE_LESS_DATA,

        DL_FT_ERROR_SET_TIMEOUT,

        // FTSTATUS
        DL_FT_ = 0xF100,
        DL_FT_INVALID_HANDLE,
        DL_FT_DEVICE_NOT_FOUND,
        DL_FT_DEVICE_NOT_OPENED,
        DL_FT_IO_ERROR,
        DL_FT_INSUFFICIENT_RESOURCES,
        DL_FT_INVALID_PARAMETER,
        DL_FT_INVALID_BAUD_RATE,

        DL_FT_DEVICE_NOT_OPENED_FOR_ERASE,
        DL_FT_DEVICE_NOT_OPENED_FOR_WRITE,
        DL_FT_FAILED_TO_WRITE_DEVICE,
        DL_FT_EEPROM_READ_FAILED,
        DL_FT_EEPROM_WRITE_FAILED,
        DL_FT_EEPROM_ERASE_FAILED,
        DL_FT_EEPROM_NOT_PRESENT,
        DL_FT_EEPROM_NOT_PROGRAMMED,
        DL_FT_INVALID_ARGS,
        DL_FT_NOT_SUPPORTED,
        DL_FT_OTHER_ERROR,
        DL_FT_DEVICE_LIST_NOT_READY,

        //----------------------------------------------------------------------
        NOT_IMPLEMENTED = 0xFFFFFFFE, //-2,
        UNKNOWN_ERROR = 0xFFFFFFFF, // - 1,
        MAX_DL_STATUS = 0xFFFFFFFF, // - 1,
        LAST_ERROR = 0xFFFFFFFF // - 1
    };
    

    public static unsafe class ais_readers
    {
        public const uint MAX_DATE_TIME_DIFF_IN_SEC = 60;
        public const int NFC_UID_MAX_LEN = 10;
        private const uint MAX_RETRY_IF_BUSY = 5;

        

        //--------------------------------------------------------------------------------------------------------------

#if WIN64

        const string DLL_PATH = "..\\..\\ufr-lib\\windows\\x86_64\\";
        const string NAME_DLL = "ais_readers-x86_64.dll";

#else
        const string DLL_PATH = "..\\..\\lib\\windows\\x86\\";

        const string NAME_DLL = "ais_readers-x86.dll";
#endif

        const string DLL_NAME = DLL_PATH + NAME_DLL;

        //--------------------------------------------------------------------------------------------------------------

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_GetLibraryVersionStr")]
        private static extern IntPtr AIS_GetLibraryVersionStr();
        public static string GetDLLVersion()
        {
            IntPtr str_ret = AIS_GetLibraryVersionStr();
            return Marshal.PtrToStringAnsi(str_ret);
        }

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "dbg_action2str")]
        private static extern IntPtr dbg_action2str(CARD_ACTION action);
        public static string action2str(CARD_ACTION action)
        {
            IntPtr str_ret = dbg_action2str(action);
            return Marshal.PtrToStringAnsi(str_ret);
        }

        //--------------------------------------------------------------------------------------------------------------

        /**
         * Function return which device will be checked.
         *
         * @return pair of Device type and ID on the bus delimited with ':'
         * 		Pairs of type:id are delimited with new line character
         */
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_List_GetDevicesForCheck")]
        private static extern IntPtr List_GetDevicesForCheck();
        public static string AIS_List_GetDevicesForCheck()
        {
            IntPtr str_ret = List_GetDevicesForCheck();
            return Marshal.PtrToStringAnsi(str_ret);
        }

        /**
         * AIS_List_EraseAllDevicesForCheck() : clear list of available devices for checking
         *
         */
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_List_EraseAllDevicesForCheck")]
        public static extern void AIS_List_EraseAllDevicesForCheck();

        /**
         * AIS_List_AddDeviceForCheck() : configure DLL - set list of available AIS readers
         *
         * @param device_type int device type by internal specification (enumeration)
         * @param device_id int Reader ID - set by Mifare Init Card
         * @return DL_STATUS
         */
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_List_AddDeviceForCheck")]
        public static extern DL_STATUS AIS_List_AddDeviceForCheck(uint device_type, uint device_id);

        /**
         * AIS_List_EraseDeviceForCheck() : configure DLL - remove reader from list for checking
         *
         * @param device_type int device type by internal specification (enumeration)
         * @param device_id int Reader ID - set by Mifare Init Card
         * @return DL_STATUS
         */
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_List_EraseDeviceForCheck")]
        public static extern DL_STATUS AIS_List_EraseDeviceForCheck(uint device_type, uint device_id);

        /**
         * AIS_List_UpdateAndGetCount()
         *
         * @return number of attached devices <br> <b>negative</b> value if error
         */
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_List_UpdateAndGetCount")]
        public static extern DL_STATUS AIS_List_UpdateAndGetCount(uint* dev_count);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_List_GetInformation")]
        private static extern DL_STATUS List_GetInformation(
                out HND_AIS hnd_device, //// assigned Handle
                out IntPtr Device_Serial, //// device serial number
                out uint Device_Type, //// device type
                out uint Device_ID, //// device identification number (master)
                out uint Device_FW_VER, //// version of firmware
                out uint Device_CommSpeed, //// communication speed
                out IntPtr ptr_FTDI_Serial, //// FTDI COM port identification
                out uint Device_isOpened, //// is Device opened
                out uint Device_Status, //// actual device status
                out uint System_Status //// actual system status
                );
        public static DL_STATUS AIS_List_GetInformation(
                out HND_AIS hnd_device, //// assigned Handle
                out string Device_Serial, //// device serial number
                out uint Device_Type, //// device type
                out uint Device_ID, //// device identification number (master)
                out uint Device_FW_VER, //// version of firmware
                out uint Device_CommSpeed, //// communication speed
                out string FTDI_Serial, //// FTDI COM port identification
                out uint Device_isOpened, //// is Device opened
                out uint Device_Status, //// actual device status
                out uint System_Status //// actual system status
            )
        {
            DL_STATUS status = DL_STATUS.DL_OK;
            uint i = 0;
            IntPtr ptr_Device_Serial;
            IntPtr ptr_FTDI_Serial; // Expected max 10 bytes for S/N

            do
            {
                status = List_GetInformation(
                    out hnd_device, //// assigned Handle
                    out ptr_Device_Serial, //// device serial number
                    out Device_Type, //// device type
                    out Device_ID, //// device identification number (master)
                    out Device_FW_VER, //// version of firmware
                    out Device_CommSpeed, //// communication speed
                    out ptr_FTDI_Serial, //// FTDI COM port identification
                    out Device_isOpened, //// is Device opened
                    out Device_Status, //// actual device status
                    out System_Status //// actual system status
                );
            } while ((status == DL_STATUS.RESOURCE_BUSY) && (i++ < MAX_RETRY_IF_BUSY));
            Device_Serial = Marshal.PtrToStringAnsi(ptr_Device_Serial);
            FTDI_Serial = Marshal.PtrToStringAnsi(ptr_FTDI_Serial);
            return status;
        }

        //--------------------------------------------------------------------------------------------------------------

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_Open")]
        private static extern DL_STATUS Open(HND_AIS hnd_device);
        public static DL_STATUS AIS_Open(HND_AIS hnd_device)
        {
            DL_STATUS status = DL_STATUS.DL_OK;
            uint i = 0;

            do
            {
                status = Open(hnd_device);
            }
            while ((status == DL_STATUS.RESOURCE_BUSY) && (i++ < MAX_RETRY_IF_BUSY));
            return status;
        }

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_Close")]
        private static extern DL_STATUS Close(HND_AIS hnd_device);
        public static DL_STATUS AIS_Close(HND_AIS hnd_device)
        {
            DL_STATUS status = DL_STATUS.DL_OK;
            uint i = 0;

            do
            {
                status = Close(hnd_device);
            }
            while ((status == DL_STATUS.RESOURCE_BUSY) && (i++ < MAX_RETRY_IF_BUSY));
            return status;
        }

        // kill object
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_Destroy")]
        private static extern DL_STATUS Destroy(HND_AIS hnd_device);
        public static DL_STATUS AIS_Destroy(HND_AIS hnd_device)
        {
            DL_STATUS status = DL_STATUS.DL_OK;
            uint i = 0;

            do
            {
                status = Destroy(hnd_device);
            }
            while ((status == DL_STATUS.RESOURCE_BUSY) && (i++ < MAX_RETRY_IF_BUSY));
            return status;
        }

        // global reset service / DLL
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_Restart")]
        private static extern DL_STATUS Restart(HND_AIS hnd_device);
        public static DL_STATUS AIS_Restart(HND_AIS hnd_device)
        {
            DL_STATUS status = DL_STATUS.DL_OK;
            uint i = 0;

            do
            {
                status = Restart(hnd_device);
            }
            while ((status == DL_STATUS.RESOURCE_BUSY) && (i++ < MAX_RETRY_IF_BUSY));
            return status;
        }

        //--------------------------------------------------------------------------------------------------------------
        // Info functions:

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_GetVersion")]
        private static extern DL_STATUS GetVersion(
                HND_AIS hnd_device,
                out uint hardware_type, // hardware type
                out uint firmware_version // firmware version
                );
        public static DL_STATUS AIS_GetVersion(
                HND_AIS hnd_device,
                out uint hardware_type, // hardware type
                out uint firmware_version // firmware version
                )
        {
            DL_STATUS status = DL_STATUS.DL_OK;
            uint i = 0;

            do
            {
                status = GetVersion(
                    hnd_device,
                    out hardware_type,
                    out firmware_version);
            }
            while ((status == DL_STATUS.RESOURCE_BUSY) && (i++ < MAX_RETRY_IF_BUSY));
            return status;
        }

        //--------------------------------------------------------------------------------------------------------------
        // Main pump:

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_MainLoop")]
        private static extern DL_STATUS MainLoop(
            HND_AIS hnd_device,
             out uint RealTimeEvents, // indicate new RealTimeEvent(s)
             out uint LogAvailable, // indicate new data in log buffer
             out uint LogUnread, // indicate new data in log buffer
             out uint CmdResponses, // indicate command finish
             out uint CmdPercent, // indicate percent of command execution
             out uint DeviceStatus, // indicate dev status
             out uint TimeoutOccurred, // debug only
             out uint Status // additional status
            );
        public static DL_STATUS AIS_MainLoop(
            HND_AIS hnd_device,
            out uint RealTimeEvents, // indicate new RealTimeEvent(s)
            out uint LogAvailable, // indicate new data in log buffer
            out uint LogUnread, // indicate new data in log buffer
            out uint CmdResponses, // indicate command finish
            out uint CmdPercent, // indicate percent of command execution
            out uint DeviceStatus, // indicate dev status
            out uint TimeoutOccurred, // debug only
            out uint Status // additional status
            )
        {
            DL_STATUS status = DL_STATUS.DL_OK;
            
            uint i = 0;
            do
            {
                uint uiLogEvent = 0, uiCmdFinished = 0, uiTimeoutOccurred = 0;

                status = MainLoop(
                    hnd_device,
            out RealTimeEvents, // indicate new RealTimeEvent(s)
            out LogAvailable, // indicate new data in log buffer
            out LogUnread, // indicate new data in log buffer
            out CmdResponses, // indicate command finish
            out CmdPercent, // indicate percent of command execution
            out DeviceStatus, // indicate dev status
            out TimeoutOccurred, // debug only
            out Status // additional status
            );

                //LogEvent = uiLogEvent != 0;
                if (uiLogEvent > 0)
                {
                    LogUnread = 1;
                }
                else LogUnread = 0;

                //CmdFinished = uiCmdFinished != 0;

                if (uiCmdFinished > 0)
                {
                    CmdResponses = 1;
                }
                else CmdResponses = 0;

                if (uiTimeoutOccurred > 0)
                {
                    TimeoutOccurred = 1;
                }
                else TimeoutOccurred = 0;

                //TimeoutOccurred = uiTimeoutOccurred != 0;
            }
            while ((status == DL_STATUS.RESOURCE_BUSY) && (i++ < MAX_RETRY_IF_BUSY));
            return status;
        }

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_DoCmd")]
        private static extern DL_STATUS DoCmd(
            HND_AIS device,
            out int cmd_finish, // bool
            out uint percent);
        public static DL_STATUS AIS_DoCmd(
            HND_AIS device,
            out bool cmd_finish,
            out uint percent)
        {
            DL_STATUS status = DL_STATUS.DL_OK;
            uint i = 0;
            int lib_cmd_finish;

            do
            {
                status = DoCmd(device, out lib_cmd_finish, out percent);
            }
            while ((status == DL_STATUS.RESOURCE_BUSY) && (i++ < MAX_RETRY_IF_BUSY));
            cmd_finish = lib_cmd_finish != 0;
            return status;
        }

        //--------------------------------------------------------------------------------------------------------------
        // Change password:

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_ChangePassword")]
        private static extern DL_STATUS ChangePassword(
            HND_AIS device,
            StringBuilder str_old_password,
            StringBuilder str_new_password);
        public static DL_STATUS AIS_ChangePassword(
            HND_AIS device,
            string str_old_password,
            string str_new_password)
        {
            DL_STATUS status = DL_STATUS.DL_OK;
            uint i = 0;
            StringBuilder ptr_old_password = new StringBuilder(str_old_password);
            StringBuilder ptr_new_password = new StringBuilder(str_new_password);

            do
            {
                status = ChangePassword(device, ptr_old_password, ptr_new_password);
            }
            while ((status == DL_STATUS.RESOURCE_BUSY) && (i++ < MAX_RETRY_IF_BUSY));
            return status;
        }
        //--------------------------------------------------------------------------------------------------------------
        // Info functions:

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_GetFTDISerial")]
        private static extern DL_STATUS GetFTDISerial(HND_AIS device, out IntPtr p_p_ftdi_serial);
        public static DL_STATUS AIS_GetFTDISerial(HND_AIS device, out string ftdi_serial)
        {
            IntPtr ptr;
            DL_STATUS status = DL_STATUS.DL_OK;
            uint i = 0;

            do
            {
                status = GetFTDISerial(device, out ptr);
            }
            while ((status == DL_STATUS.RESOURCE_BUSY) && (i++ < MAX_RETRY_IF_BUSY));
            ftdi_serial = Marshal.PtrToStringAnsi(ptr);
            return status;
        }

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_BatteryGetInfo")]
        private static extern DL_STATUS BatteryGetInfo(
            HND_AIS device,
            out uint battery_status,
            out uint battery_available_percent);
        public static DL_STATUS AIS_BatteryGetInfo(
            HND_AIS device,
            out uint battery_status,
            out uint battery_available_percent)
        {
            DL_STATUS status = DL_STATUS.DL_OK;
            uint i = 0;

            do
            {
                status = BatteryGetInfo(device, out battery_status, out battery_available_percent);
            }
            while ((status == DL_STATUS.RESOURCE_BUSY) && (i++ < MAX_RETRY_IF_BUSY));
            return status;
        }

        //--------------------------------------------------------------------------------------------------------------
        // Time functions:

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_GetTime")]
        private static extern DL_STATUS GetTime(HND_AIS device, out UInt64 unixTime, out int timezone, out int dst, out int dst_bias, int additional); // timezone and dst_bias in min.
        public static DL_STATUS AIS_GetTime(HND_AIS device, out DateTime date_time, out int timezone, out int is_dst, out int offset, int additional) // timezone and dst_bias in min.
        {
            DL_STATUS status = DL_STATUS.DL_OK;
            uint i = 0;
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            UInt64 unixTime;
            int dst;

            do
            {
                status = GetTime(device, out unixTime, out timezone, out dst, out offset, additional);
            }
            while ((status == DL_STATUS.RESOURCE_BUSY) && (i++ < MAX_RETRY_IF_BUSY));
            
                is_dst = dst;
            
            date_time = DateTime.SpecifyKind(epoch.AddSeconds(unixTime), DateTimeKind.Utc);
            return status;
        }

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_SetTime")]
        private static extern DL_STATUS SetTime(HND_AIS device, StringBuilder password, UInt64 unixTime, int timezone, int dst, int offset, int additional); // timezone and dst_bias in min.
        public static DL_STATUS AIS_SetTime(HND_AIS device, string str_password, DateTime date_time, int timezone, int dst, int offset, int additional) // timezone and dst_bias in min.
        {
            DL_STATUS status = DL_STATUS.DL_OK;
            uint i = 0;
            int int_dst = 0;
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan span = (date_time - epoch);
            UInt64 unixTime = (UInt64)span.TotalSeconds;

            if (dst != 0)
                int_dst = 1;
            StringBuilder password = new StringBuilder(str_password);
            do
            {
                status = SetTime(device, password, unixTime, timezone, int_dst, offset, additional);
            }
            while ((status == DL_STATUS.RESOURCE_BUSY) && (i++ < MAX_RETRY_IF_BUSY));
            return status;
        }

        //--------------------------------------------------------------------------------------------------------------
        // RTE functions

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_ReadRTE_Count")]
        public static extern int AIS_ReadRTE_Count(HND_AIS device);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_ReadRTE")]
        private static extern DL_STATUS ReadRTE(
                HND_AIS device,
                out uint log_index,
                out uint log_action,
                out uint log_reader_id,
                out uint log_card_id,
                out uint log_system_id,
                [In, Out] byte[] nfc_uid, // NFC_UID_MAX_LEN = 10
                out uint nfc_uid_len,
                out UInt64 unixTime // time_t
            );
        public static DL_STATUS AIS_ReadRTE(
                HND_AIS device,
                out uint log_index,
                out uint log_action,
                out uint log_reader_id,
                out uint log_card_id,
                out uint log_system_id,
                [In, Out] byte[] nfc_uid, // NFC_UID_MAX_LEN = 10
                out uint nfc_uid_len,
                out DateTime date_time
            )
        {
            DL_STATUS status = DL_STATUS.DL_OK;
            uint i = 0;
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            UInt64 unixTime;

            do
            {
                status = ReadRTE(device, out log_index, out log_action, out log_reader_id, out log_card_id, out log_system_id, nfc_uid, out nfc_uid_len, out unixTime);
            }
            while ((status == DL_STATUS.RESOURCE_BUSY) && (i++ < MAX_RETRY_IF_BUSY));
            date_time = epoch.AddSeconds(unixTime);
            return status;
        }

        //--------------------------------------------------------------------------------------------------------------

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_UnreadLOG_Get")]
        private static extern DL_STATUS GetUnread(
            HND_AIS device,
            out uint log_index,
            out uint log_action,
            out uint log_reader_id,
            out uint log_card_id,
            out uint log_system_id,
            [In, Out] byte[] nfc_uid, // NFC_UID_MAX_LEN = 10
            out uint nfc_uid_len,
            out UInt64 unixTime // time_t
        );
        public static DL_STATUS AIS_UnreadLOG_Get(
                HND_AIS device,
                out uint log_index,
                out uint log_action,
                out uint log_reader_id,
                out uint log_card_id,
                out uint log_system_id,
                [In, Out] byte[] nfc_uid, // NFC_UID_MAX_LEN = 10
                out uint nfc_uid_len,
                out DateTime date_time
            )
        {
            DL_STATUS status = DL_STATUS.DL_OK;
            uint i = 0;
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            UInt64 unixTime;

            do
            {
                status = GetUnread(device, out log_index, out log_action, out log_reader_id,
                                out log_card_id, out log_system_id, nfc_uid, out nfc_uid_len, out unixTime);
            }
            while ((status == DL_STATUS.RESOURCE_BUSY) && (i++ < MAX_RETRY_IF_BUSY));
            date_time = epoch.AddSeconds(unixTime);
            return status;
        }

        //--------------------------------------------------------------------------------------------------------------
        // LOG functions

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_GetLog")]
        private static extern DL_STATUS GetLog(HND_AIS device, StringBuilder password);
        public static DL_STATUS AIS_GetLog(HND_AIS device, string str_password)
        {
            DL_STATUS status = DL_STATUS.DL_OK;
            uint i = 0;
            StringBuilder password = new StringBuilder(str_password);

            do
            {
                status = GetLog(device, password);
            }
            while ((status == DL_STATUS.RESOURCE_BUSY) && (i++ < MAX_RETRY_IF_BUSY));
            return status;
        }

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_GetLog_Set")]
        private static extern DL_STATUS GetLog_Set(HND_AIS device, StringBuilder password);
        public static DL_STATUS AIS_GetLog_Set(HND_AIS device, string str_password)
        {
            DL_STATUS status = DL_STATUS.DL_OK;
            uint i = 0;
            StringBuilder password = new StringBuilder(str_password);

            do
            {
                status = GetLog_Set(device, password);
            }
            while ((status == DL_STATUS.RESOURCE_BUSY) && (i++ < MAX_RETRY_IF_BUSY));
            return status;
        }

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_GetLogByIndex")]
        private static extern DL_STATUS GetLogByIndex(HND_AIS device, StringBuilder password,
                                                      uint start_index, uint end_index);
        public static DL_STATUS AIS_GetLogByIndex(HND_AIS device, string str_password,
                                                  uint start_index, uint end_index)
        {
            DL_STATUS status = DL_STATUS.DL_OK;
            uint i = 0;
            StringBuilder password = new StringBuilder(str_password);

            do
            {
                status = GetLogByIndex(device, password, start_index, end_index);
            }
            while ((status == DL_STATUS.RESOURCE_BUSY) && (i++ < MAX_RETRY_IF_BUSY));
            return status;
        }

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_GetLogByTime")]
        private static extern DL_STATUS GetLogByTime(HND_AIS device, StringBuilder password,
                                                      UInt64 unix_time_from, UInt64 unix_time_to);
        public static DL_STATUS AIS_GetLogByTime(HND_AIS device, string str_password,
                                                  DateTime timeFrom, DateTime timeTo)
        {
            DL_STATUS status = DL_STATUS.DL_OK;
            uint i = 0;
            StringBuilder password = new StringBuilder(str_password);
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan span = timeFrom - epoch;
            UInt64 unixTimeFrom = (UInt64)span.TotalSeconds;
            span = timeTo - epoch;
            UInt64 unixTimeTo = (UInt64)span.TotalSeconds;
            span = timeTo - epoch;
            do
            {
                status = GetLogByTime(device, password, unixTimeFrom, unixTimeTo);
            }
            while ((status == DL_STATUS.RESOURCE_BUSY) && (i++ < MAX_RETRY_IF_BUSY));
            return status;
        }

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_ClearLog")]
        public static extern DL_STATUS AIS_ClearLog(HND_AIS device);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_ReadLog_Count")]
        public static extern uint AIS_ReadLog_Count(HND_AIS device);
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_UnreadLOG_Ack")]
        public static extern DL_STATUS AIS_UnreadLOG_Ack(HND_AIS device, uint records_to_ack);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_ReadLog")]
        private static extern DL_STATUS ReadLog(
                HND_AIS device,
                out uint log_index,
                out uint log_action,
                out uint log_reader_id,
                out uint log_card_id,
                out uint log_system_id,
                [In, Out] byte[] nfc_uid, // NFC_UID_MAX_LEN = 10
                out uint nfc_uid_len,
                out UInt64 unixTime // time_t
                );
        public static DL_STATUS AIS_ReadLog(
                HND_AIS device,
                out uint log_index,
                out uint log_action,
                out uint log_reader_id,
                out uint log_card_id,
                out uint log_system_id,
                [In, Out] byte[] nfc_uid, // NFC_UID_MAX_LEN = 10
                out uint nfc_uid_len,
                out DateTime date_time
           )
        {
            DL_STATUS status = DL_STATUS.DL_OK;
            uint i = 0;
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            UInt64 unixTime;

            do
            {
                status = ReadLog(device, out log_index, out log_action, out log_reader_id,
                                out log_card_id, out log_system_id, nfc_uid, out nfc_uid_len, out unixTime);
            }
            while ((status == DL_STATUS.RESOURCE_BUSY) && (i++ < MAX_RETRY_IF_BUSY));
            date_time = epoch.AddSeconds(unixTime);
            return status;
        }

        //--------------------------------------------------------------------------------------------------------------
        // Blacklist functions

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_Blacklist_Write")]
        private static extern DL_STATUS Blacklist_Write(HND_AIS device, StringBuilder password,
                                                        StringBuilder csv_blacklist);
        public static DL_STATUS AIS_Blacklist_Write(HND_AIS device, string str_password, string str_csv_blacklist)
        {
            DL_STATUS status = DL_STATUS.DL_OK;
            uint i = 0;
            StringBuilder password = new StringBuilder(str_password);
            StringBuilder csv_blacklist = new StringBuilder(str_csv_blacklist);

            do
            {
                status = Blacklist_Write(device, password, csv_blacklist);
            }
            while ((status == DL_STATUS.RESOURCE_BUSY) && (i++ < MAX_RETRY_IF_BUSY));
            return status;
        }

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_Blacklist_GetSize")]
        private static extern DL_STATUS Blacklist_GetSize(HND_AIS device, StringBuilder password, out int size);
        public static DL_STATUS AIS_Blacklist_GetSize(HND_AIS device, string str_password, out int size)
        {
            DL_STATUS status = DL_STATUS.DL_OK;
            uint i = 0;
            StringBuilder password = new StringBuilder(str_password);

            do
            {
                status = Blacklist_GetSize(device, password, out size);
            }
            while ((status == DL_STATUS.RESOURCE_BUSY) && (i++ < MAX_RETRY_IF_BUSY));
            return status;
        }

        //[DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_Blacklist_Read")]
        //public static extern DL_STATUS AIS_Blacklist_Read(HND_AIS device, StringBuilder csv_blacklist);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_Blacklist_Read")]
        private static extern DL_STATUS Blacklist_Read(HND_AIS device, StringBuilder password, out IntPtr ptr_blacklist);
        public static DL_STATUS AIS_Blacklist_Read(HND_AIS device, string str_password, out string str_blacklist)
        {
            DL_STATUS status = DL_STATUS.DL_OK;
            uint i = 0;
            StringBuilder password = new StringBuilder(str_password);
            IntPtr ptr_blacklist;

            do
            {
                status = Blacklist_Read(device, password, out ptr_blacklist);
            }
            while ((status == DL_STATUS.RESOURCE_BUSY) && (i++ < MAX_RETRY_IF_BUSY));

            str_blacklist = Marshal.PtrToStringAnsi(ptr_blacklist); ;
            return status;
        }

        //--------------------------------------------------------------------------------------------------------------
        // WhiteList functions
        /**
         *
         * @param device : see info about device handle
         * @param password : see info about password
         * @param str_csv_whitelist : eg. "54:A3:34:12, 12.34.56.78, 01234567"
         * 			HEX pairs in UID can be delimited with: ':' or '.' or none
         * 			UID separators: ',' or ';' or other white space
         * 			! NULL or blank string erase white list in device
         * @return
         */
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_Whitelist_Write")]
        private static extern DL_STATUS Whitelist_Write(HND_AIS device, StringBuilder str_password, StringBuilder whitelist);
        public static DL_STATUS AIS_Whitelist_Write(HND_AIS device, string str_password, string str_whitelist)
        {
            DL_STATUS status = DL_STATUS.DL_OK;
            uint i = 0;
            StringBuilder password = new StringBuilder(str_password);
            StringBuilder whitelist = new StringBuilder(str_whitelist);

            do
            {
                status = Whitelist_Write(device, password, whitelist);
            }
            while ((status == DL_STATUS.RESOURCE_BUSY) && (i++ < MAX_RETRY_IF_BUSY));
            return status;
        }

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_Whitelist_Read")]
        private static extern DL_STATUS Whitelist_Read(HND_AIS device, StringBuilder password, out IntPtr ptr_whitelist);
        public static DL_STATUS AIS_Whitelist_Read(HND_AIS device, string str_password, out string str_whitelist)
        {
            DL_STATUS status = DL_STATUS.DL_OK;
            uint i = 0;
            StringBuilder password = new StringBuilder(str_password);
            IntPtr ptr_whitelist;

            do
            {
                status = Whitelist_Read(device, password, out ptr_whitelist);
            }
            while ((status == DL_STATUS.RESOURCE_BUSY) && (i++ < MAX_RETRY_IF_BUSY));

            str_whitelist = Marshal.PtrToStringAnsi(ptr_whitelist); ;
            return status;
        }

        //--------------------------------------------------------------------------------------------------------------
        // XRCA Base HD SDK

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_LockOpen")]
        public static extern DL_STATUS AIS_LockOpen(HND_AIS device, uint pulse_duration);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_RelayStateSet")]
        public static extern DL_STATUS AIS_RelayStateSet(HND_AIS device, uint state);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_GetIoState")]
        public static extern DL_STATUS AIS_GetIoState(HND_AIS device, out uint intercom, out uint door, out uint relay_state);

        /**
         *
         * set value for light: 0 = off, not null = on
         * @param device
         * @param green_master control green light on master unit
         * @param red_master control red light on master unit
         * @param green_slave control green light on slave unit
         * @param red_slave control red light on slave unit
         * @return
         */
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_LightControl")]
        public static extern DL_STATUS AIS_LightControl(HND_AIS device,
                                                        uint green_master, uint red_master,
                                                        uint green_slave, uint red_slave);

        //--------------------------------------------------------------------------------------------------------------

        //----------------------------------------- functions ---------------------------------------------------------//

        public static void print_menu()
        {

            Globals global = new Globals();

            Console.WriteLine(" +------------------------------------------------+");
            Console.WriteLine(" |          Ais Readers app (version : 1.0)       |");
            string dll_version = GetDLLVersion();
                Console.Write(" |       ");
                Console.Write(dll_version);
            Console.WriteLine("      |");
            Console.WriteLine(" +------------------------------------------------+");
            Console.WriteLine("                              For exit, hit escape.");
            Console.WriteLine(" --------------------------------------------------");
            Console.WriteLine("  (1) - Read whitelist from database");
            Console.WriteLine("  (2) - Read blacklist from database");
            Console.WriteLine("  (3) - Get logs");
            Console.WriteLine("  (4) - Read whitelist from reader");
            Console.WriteLine("  (5) - Read blacklist from reader");
            Console.WriteLine("  (6) - Send all logs to database");
            Console.WriteLine("  (7) - Set time to devices");
            Console.WriteLine("  (8) - Get time from devices");
            Console.WriteLine("  (I) - Get logs by index");
            Console.WriteLine("  (T) - Get logs by time");

            

            //debug file write//
            File.AppendAllText(global.debug_file_path," +------------------------------------------------+" + Environment.NewLine);
            File.AppendAllText(global.debug_file_path," |          Ais Readers app (version : 1.0)       |" + Environment.NewLine);            
            File.AppendAllText(global.debug_file_path, " |       ");
            File.AppendAllText(global.debug_file_path,dll_version);
            File.AppendAllText(global.debug_file_path,"      |" + Environment.NewLine);
            File.AppendAllText(global.debug_file_path," +------------------------------------------------+" + Environment.NewLine);
            File.AppendAllText(global.debug_file_path,"                              For exit, hit escape." + Environment.NewLine);
            File.AppendAllText(global.debug_file_path," --------------------------------------------------" + Environment.NewLine);
            File.AppendAllText(global.debug_file_path,"  (1) - Read whitelist from database" + Environment.NewLine);
            File.AppendAllText(global.debug_file_path,"  (2) - Read blacklist from database" + Environment.NewLine);
            File.AppendAllText(global.debug_file_path,"  (3) - Get logs" + Environment.NewLine);
            File.AppendAllText(global.debug_file_path,"  (4) - Read whitelist from reader" + Environment.NewLine);
            File.AppendAllText(global.debug_file_path,"  (5) - Read blacklist from reader" + Environment.NewLine);
            File.AppendAllText(global.debug_file_path,"  (6) - Send all logs to database" + Environment.NewLine);
            File.AppendAllText(global.debug_file_path,"  (7) - Set time to devices" + Environment.NewLine);
            File.AppendAllText(global.debug_file_path,"  (8) - Get time from devices" + Environment.NewLine);
            File.AppendAllText(global.debug_file_path,"  (I) - Get logs by index" + Environment.NewLine);
            File.AppendAllText(global.debug_file_path,"  (T) - Get logs by time" + Environment.NewLine);



        }

        public static void PrintRealTimeEventTable()
        {
            Globals global = new Globals();

            Console.WriteLine("");
            Console.WriteLine("=================================================================================================================================================================");
            Console.WriteLine("   Log index  |          Log action            |  Reader ID  | Card ID | System ID |       UID          | UID length |                  Time               |Type|");
            Console.WriteLine("=================================================================================================================================================================");

            File.AppendAllText(global.debug_file_path, Environment.NewLine);
            File.AppendAllText(global.debug_file_path, "=================================================================================================================================================================" + Environment.NewLine);
            File.AppendAllText(global.debug_file_path, "   Log index  |          Log action            |  Reader ID  | Card ID | System ID |       UID          | UID length |                  Time               |Type|" + Environment.NewLine);
            File.AppendAllText(global.debug_file_path, "=================================================================================================================================================================" + Environment.NewLine);
                        
        }

        public static void OpenAllDevicesAndReadLists(ref Globals Global_vars)
        {
            uint pDevice_Type, pDevice_ID, pDevice_FW_VER, pDevice_CommSpeed, pDevice_isOpened, pDevice_Status, pSystem_Status;
            string pDevice_Serial, pDevice_FTDI_Serial,line = "";
          
            int ID_from_file = 0;

            DL_STATUS status;

            Console.Write("Checking readers.ini ... ");
            File.AppendAllText(Global_vars.debug_file_path, "Checking readers.ini..." + Environment.NewLine);
            
            string readers_path = "readers.ini";
            if (Global_vars.gui_on)
                readers_path = "../../../" + readers_path;

            StreamReader file = new StreamReader(readers_path);

            while ((line = file.ReadLine()) != null)
            {
                ID_from_file = Convert.ToInt32(line);

                status = AIS_List_AddDeviceForCheck(11, (uint)ID_from_file);
            }

            file.Close();

            uint num_dev = Global_vars.devices;

            AIS_List_UpdateAndGetCount(&num_dev);

            Global_vars.devices = num_dev;

            Console.Write("number of devices: ");
            File.AppendAllText(Global_vars.debug_file_path, "number of devices: " + Global_vars.devices + Environment.NewLine);

            Console.WriteLine(Global_vars.devices);          

            for (int i = 0; i < num_dev; i++)
            {
                AIS_List_GetInformation(out Global_vars.handle[i], out pDevice_Serial, out pDevice_Type, out pDevice_ID, out pDevice_FW_VER, out pDevice_CommSpeed, out pDevice_FTDI_Serial, out pDevice_isOpened, out pDevice_Status, out pSystem_Status);

                Global_vars.id_handle[pDevice_ID] = Global_vars.handle[i];

                Global_vars.device_IDs[i] = pDevice_ID;

                status = AIS_Open(Global_vars.handle[i]);

                if (status > 0)
                {
                    Console.Write("Reader whose ID is: ");
                    Console.Write(Global_vars.device_IDs[i]);
                    Console.Write(" could not be opened, status: ");
                    Console.Write(status);
                    Console.WriteLine(" ");

                    File.AppendAllText(Global_vars.debug_file_path, "Reader whose ID is: " + Global_vars.device_IDs[i] + " could not be opened, status: " + status.ToString() + Environment.NewLine); 
                  
                }
                else
                {
                    Console.Write("Reader whose ID is: ");
                    Console.Write(Global_vars.device_IDs[i]);
                    Console.Write(" succesfully opened, status: ");
                    Console.Write(status);
                    Console.WriteLine(" ");

                    File.AppendAllText(Global_vars.debug_file_path, "Reader whose ID is: " + Global_vars.device_IDs[i] + " successfully opened, status: " + status.ToString() + Environment.NewLine);

                }

                // ReadBlacklistFromDB(ref Global_vars.device_IDs[i], ref Global_vars); / test speed
                // ReadWhitelistFromDB(ref Global_vars.device_IDs[i], ref Global_vars); / test speed
            }            
        }

        public static void AppLoop(ref Globals Global_vars)
        {

            DateTime time_now = DateTime.Now;
            DateTime now = DateTime.Now;
            DL_STATUS status = 0;

            ais_readers.ReadSettings(ref Global_vars);
            
            int pos = 0;
            
            String time_str = "";      

            time_str = time_now.ToString();

            time_now = DateTime.Now;

            bool run_loop = true;

            DateTime waiting_time = time_now.AddMinutes(Convert.ToDouble(Global_vars.str_minutes_check));

            pos = time_str.IndexOf(":");

            time_str = time_str.Substring(pos - 2);

            if (Global_vars.gui_on == false)
            {
                if (Console.KeyAvailable == false)
                {
                    run_loop = true;
                }
            } else if (Global_vars.gui_on == true)
            {
                if (Global_vars.gui_command == "0")
                {
                    run_loop = true;
                }
                else
                {
                    run_loop = false;
                }
            }

            while (run_loop == true)
            {
                ReadSettings(ref Global_vars);

                time_now = DateTime.Now;

                DateTime start_time = Convert.ToDateTime(Global_vars.str_time_to_send);

                DateTime postponed_logs = start_time.AddHours(Convert.ToDouble(Global_vars.send_hours));

                bool writing_is_true = (time_now.Hour == waiting_time.Hour) && (time_now.Minute == waiting_time.Minute) && (time_now.Second == waiting_time.Second);

                if (writing_is_true == true)
                {
                    ///////////////////////////////////// ===Whitelist=== ////////////////////////////////////

                    for (int i = 0; i < Global_vars.devices; i++)
                    {
                        string db_wl = ReadWhitelistFromDB(ref Global_vars.device_IDs[i], ref Global_vars);

                        string reader_wl = ReadWhitelistFromReader(Global_vars.handle[i], ref Global_vars);

                        if (db_wl != reader_wl)
                        {
                            Console.Write("Whitelist in database changed for reader ID: ");
                            Console.Write(Global_vars.device_IDs[i]);
                            Console.WriteLine("], updating whitelist");


                            File.AppendAllText(Global_vars.debug_file_path, "Whitelist in database changed for reader ID: " + Global_vars.device_IDs[i] + "] updating whitelist" + Environment.NewLine);

                            status = AIS_Whitelist_Write(Global_vars.handle[i], Global_vars.password, db_wl);

                            if (status != 0)
                            {
                                Console.Write("Error while writing whitelist to reader ID [");
                                Console.Write(Global_vars.device_IDs[i]);
                                Console.Write("], status: ");
                                Console.WriteLine((Enum.GetName(typeof(DL_STATUS), status)));

                                File.AppendAllText(Global_vars.debug_file_path, "Error while writing whitelist to reader ID [" + Global_vars.device_IDs[i] + "], status: " + status.ToString() + Environment.NewLine);

                            }
                            else
                            {
                                Console.Write("Whitelist succesfully written to reader ID [");
                                Console.Write(Global_vars.device_IDs[i]);
                                Console.Write("], status: ");
                                Console.WriteLine((Enum.GetName(typeof(DL_STATUS), status)));

                                File.AppendAllText(Global_vars.debug_file_path, "Whitelist succesfully written to reader ID [" + Global_vars.device_IDs[i] + "], status" + status.ToString() + Environment.NewLine);
                            }
                        }

                    }
                    ///////////////////////////////////// ===Blacklist=== ////////////////////////////////////

                    for (int i = 0; i < Global_vars.devices; i++)
                    {
                        string db_bl = ReadBlacklistFromDB(ref Global_vars.device_IDs[i], ref Global_vars);

                        string reader_bl = ReadBlacklistFromReader(Global_vars.handle[i], ref Global_vars);

                        if (db_bl != reader_bl)
                        {
                            Console.Write("Blacklist in database has changed for reader ID: ");
                            Console.Write(Global_vars.device_IDs[i]);
                            Console.WriteLine("], updating blacklist");

                            File.AppendAllText(Global_vars.debug_file_path, "Blacklist in database changed for reader ID: " + Global_vars.device_IDs[i] + "] updating blacklist" + Environment.NewLine);


                            status = AIS_Blacklist_Write(Global_vars.handle[i], Global_vars.password, db_bl);

                            if (status != 0)
                            {
                                Console.Write("Error while writing blacklist to reader ID [");
                                Console.Write(Global_vars.device_IDs[i]);
                                Console.Write("], status: ");
                                Console.WriteLine((Enum.GetName(typeof(DL_STATUS), status)));

                                File.AppendAllText(Global_vars.debug_file_path, "Error while writing blacklist to reader ID [" + Global_vars.device_IDs[i] + "], status: " + status.ToString() + Environment.NewLine);

                            }
                            else
                            {
                                Console.Write("Blacklist succesfully written to reader ID [");
                                Console.Write(Global_vars.device_IDs[i]);
                                Console.Write("], status: ");
                                Console.WriteLine((Enum.GetName(typeof(DL_STATUS), status)));

                                File.AppendAllText(Global_vars.debug_file_path, "Blacklist succesfully written to reader ID [" + Global_vars.device_IDs[i] + "], status" + status.ToString() + Environment.NewLine);

                            }
                        }
                    }

                    for (int i = 0; i < Global_vars.devices; i++)
                    {

                        SetDevicesTime(Global_vars.handle[i], Global_vars.device_IDs[i], ref Global_vars);

                    }

                    waiting_time = time_now.AddMinutes(Convert.ToDouble(Global_vars.str_minutes_check));
                }

                bool sending_logs_true = (start_time.Hour == time_now.Hour) && (start_time.Minute == time_now.Minute) && (start_time.Second == time_now.Second);

                bool postponed_logs_true = (postponed_logs.Hour == time_now.Hour) && (postponed_logs.Minute == time_now.Minute) && (postponed_logs.Second == time_now.Second);

                if (sending_logs_true == true || postponed_logs_true == true)
                {
                    SendAllLogs(Global_vars.handle, ref Global_vars);

                    if (Global_vars.gui_on ==false)
                         PrintRealTimeEventTable();
                }

                if (Global_vars.gui_on == true)
                {
                    Console.WriteLine("alive");
                    System.Threading.Thread.Sleep(20); 
                }

                SendRTE(Global_vars.handle, ref Global_vars.devices, ref Global_vars);
             
                if (Global_vars.gui_on == true)
                {
                    Global_vars.gui_command = Console.ReadLine().ToString(); //this line reads sent command, 
                    if (Global_vars.gui_command != "0")
                    {
                        gui_menu(Global_vars.gui_command, ref Global_vars);
                    }
                    else
                    {
                        AppLoop(ref Global_vars);  
                    }
                    
                } else if (Global_vars.gui_on == false)
                {
                    if (Console.KeyAvailable)
                    {
                        run_loop = false;
                    }
                } 
            }
        }

        public static void SendRTE(UIntPtr[] handle, ref uint device_count, ref Globals Global_vars)
        {
            UInt64 rte_count = 0;
            uint RealTimeEvents = 0, LogAvailable = 0, LogUnread = 0, cmdResponses = 0, cmdPercent = 0, DeviceStatus = 0, TimeoutOccurred = 0, int_status = 0;
            uint log_index = 0, log_action = 0, log_reader_id = 0, log_card_id = 0, log_system_id = 0, nfc_uid_len = 0;
            byte[] nfc_uid = new byte[7];
            DateTime timestamp;       
       
            DL_STATUS rte_status;
            DL_STATUS url_status;

            do
            {
                for (int i = 0; i < device_count; i++)
                {
                    String to_send = "";

                    MainLoop(handle[i], out RealTimeEvents, out LogAvailable, out LogUnread, out cmdResponses, out cmdPercent, out DeviceStatus, out TimeoutOccurred, out int_status);

                    rte_count = (ulong)AIS_ReadRTE_Count(handle[i]);

                    if (RealTimeEvents > 0)
                    {
                        rte_status = AIS_ReadRTE(handle[i], out log_index, out log_action, out log_reader_id, out log_card_id, out log_system_id, nfc_uid, out nfc_uid_len, out timestamp);
                      if (rte_status == DL_STATUS.DL_OK) { 
                         if (Global_vars.gui_on == false)
                            { 
                            Console.Write("     {0,-15}", log_index);
                            Console.Write("{0,-32}", Enum.GetName(typeof(CARD_ACTION), log_action));
                            Console.Write("{0,-15}", log_reader_id);
                            Console.Write("{0,-10}", log_card_id);
                            Console.Write("{0,-8}", log_system_id);
                            Console.Write("{0,-22}", BitConverter.ToString(nfc_uid));
                            Console.Write("  {0,-20}", nfc_uid_len);
                            Console.Write("{0,-30}", timestamp);
                            Console.WriteLine("{0,-5}", "R");
                            } else { 
                            Console.WriteLine("RTE SENT");
                            Console.WriteLine("log index:{0}", log_index.ToString());
                            Console.WriteLine("log action:{0}", Enum.GetName(typeof(CARD_ACTION), log_action));
                            Console.WriteLine("log reader id:{0}", log_reader_id.ToString());
                            Console.WriteLine("log card id:{0}", log_card_id.ToString());
                            Console.WriteLine("log system id:{0}", log_system_id.ToString());
                            Console.WriteLine("nfc uid:{0}", BitConverter.ToString(nfc_uid));
                            Console.WriteLine("nfc uid len:{0}", nfc_uid_len.ToString());
                            Console.WriteLine("timetamp:{0}", timestamp.ToString());
                            Console.WriteLine("log type:{0}", "R");
                            Console.WriteLine("RTE READ");
                            }
                        // 23:54
                            File.AppendAllText(Global_vars.debug_file_path,
                            log_index + "           |" + Enum.GetName(typeof(CARD_ACTION), log_action) + "|    " +
                            log_reader_id + "       | "
                            + log_card_id + "     |    "
                            + log_system_id + "     |    "
                            + BitConverter.ToString(nfc_uid) + "                |    "
                            + nfc_uid_len + "       |"
                            + timestamp + "  | " + "     R" + Environment.NewLine);


                        to_send = "";

                        to_send = "logindex=" + log_index.ToString() + "&logaction=" + Enum.GetName(typeof(CARD_ACTION), log_action)
                            + "&readerid=" + log_reader_id.ToString() + "&cardid=" + log_card_id.ToString()
                            + "&systemid=" + log_system_id.ToString() + "&nfcuid=" + BitConverter.ToString(nfc_uid)
                            + "&uidlength=" + nfc_uid_len.ToString() + "&timestamp=" + timestamp.ToString();

                        using (WebClient wc = new WebClient())
                        {
                                try { 
                            wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                            string HtmlResult = wc.UploadString("http://" + Global_vars.server_address + "/AisReaders//addrte.php/", to_send);
                                } catch (Exception e)
                                {
                                    
                                        Console.WriteLine("Connection to database couldn't be established, check DB and try again!");
                                   
                                }
                        }
                      }
                    }
                    else
                    {
                        if (LogUnread > 0 && RealTimeEvents == 0)
                        {
                           
                            url_status = AIS_UnreadLOG_Get(handle[i], out log_index, out log_action, out log_reader_id, out log_card_id, out log_system_id, nfc_uid, out nfc_uid_len, out timestamp);                                  //Console.Write("handle[i].ToString():");
                         if (url_status == DL_STATUS.DL_OK)
                         {
                                if (Global_vars.gui_on == false)
                                {
                                    Console.Write("     {0,-15}", log_index);
                                    Console.Write("{0,-32}", Enum.GetName(typeof(CARD_ACTION), log_action));
                                    Console.Write("{0,-15}", log_reader_id);
                                    Console.Write("{0,-10}", log_card_id);
                                    Console.Write("{0,-8}", log_system_id);
                                    Console.Write("{0,-22}", BitConverter.ToString(nfc_uid));
                                    Console.Write("  {0,-20}", nfc_uid_len);
                                    Console.Write("{0,-30}", timestamp);
                                    Console.WriteLine("{0,-5}", "U");
                                }
                                else
                                {
                                    Console.WriteLine("RTE SENT");
                                    Console.WriteLine("log index:{0}", log_index.ToString());
                                    Console.WriteLine("log action:{0}", Enum.GetName(typeof(CARD_ACTION), log_action));
                                    Console.WriteLine("log reader id:{0}", log_reader_id.ToString());
                                    Console.WriteLine("log card id:{0}", log_card_id.ToString());
                                    Console.WriteLine("log system id:{0}", log_system_id.ToString());
                                    Console.WriteLine("nfc uid:{0}", BitConverter.ToString(nfc_uid));
                                    Console.WriteLine("nfc uid len:{0}", nfc_uid_len.ToString());
                                    Console.WriteLine("timetamp:{0}", timestamp.ToString());
                                    Console.WriteLine("log type:{0}", "U");
                                    Console.WriteLine("RTE READ");
                                }                               

                           File.AppendAllText(Global_vars.debug_file_path,
                           log_index + "           |" + Enum.GetName(typeof(CARD_ACTION), log_action) + "|    " +
                           log_reader_id + "       | "
                           + log_card_id + "     |    "
                           + log_system_id + "     |    "
                           + BitConverter.ToString(nfc_uid) + "                |    "
                           + nfc_uid_len + "       |"
                           + timestamp + "  | " + "     U" + Environment.NewLine);
                                
                            to_send = "";
                                  
                            to_send = "logindex=" + log_index.ToString() + "&logaction=" + Enum.GetName(typeof(CARD_ACTION), log_action)
                                + "&readerid=" + log_reader_id.ToString()
                                + "&cardid=" + log_card_id.ToString() + "&systemid=" + log_system_id.ToString()
                                + "&nfcuid=" + BitConverter.ToString(nfc_uid) + "&uidlength=" + nfc_uid_len.ToString() + "&timestamp=" + timestamp.ToString();

                            using (WebClient wc = new WebClient())
                            {
                                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                                    try { 
                                string HtmlResult = wc.UploadString("http://"+Global_vars.server_address+"/AisReaders/addunreadlog.php/", to_send);
                                    
                                    
                                if (HtmlResult == "ok") 
                                {
                                    url_status = AIS_UnreadLOG_Ack(handle[i], 1);
                                    
                                } else if (HtmlResult.Length >= 15) // avoiding exception *System.Out of range* this way when creating 'duplicate' string//
                                    {
                                    string duplicate = HtmlResult.Substring(0, 15);
                                    
                                    url_status = AIS_UnreadLOG_Ack(handle[i], 1);
                                }
                                    }
                                    catch (Exception e)
                                    {
                                      
                                        Console.WriteLine("Connection to database couldn't be established, check DB and try again!");

                                        break;
                                       
                                    }
                                }
                         }
                          
                        }
                    }

                    to_send = "";
                 }                

            } while (rte_count > 0);


        }

        public static void GetLogs(UIntPtr[] handle, ref Globals Global_vars)
        {
            DL_STATUS status = 0;
            DL_STATUS status_last = 0;

            for (int i = 0; i < Global_vars.devices; i++)
            {
                
            uint log_index = 0, log_action = 0, log_reader_id = 0, log_card_id = 0, log_system_id = 0, nfc_uid_len = 0;
            DateTime timestamp;
            byte[] nfc_uid = new byte[10];
            uint RealTimeEvents = 0, LogAvailable = 0, LogUnread = 0, cmdResponses = 0, cmdPercent = 0, DeviceStatus = 0, TimeoutOccurred = 0, int_status = 0;

                Global_vars.cmd_finish = false;

                AIS_ClearLog(handle[i]);

                status = AIS_GetLog(handle[i], Global_vars.password);
                    if(status > 0)
                {
                    return;
                }

                do
                {
                    status = MainLoop(handle[i], out RealTimeEvents, out LogAvailable, out LogUnread, out cmdResponses, out cmdPercent, out DeviceStatus, out TimeoutOccurred, out int_status);

                    Console.Write("\rDownloading Logs ... ");

                    Console.Write(cmdPercent);
                        
                    if (status > 0)
                    {
                        if (status_last != status)
                        {
                            status_last = status;
                        }

                        return;
                    }

                    if (cmdResponses > 0)
                    {
                        Global_vars.cmd_finish = true;

                        Console.WriteLine("");
                        Console.Write("Device[");
                        Console.Write(Global_vars.device_IDs[i]);
                        Console.WriteLine("] logs:");

                        if (Global_vars.gui_on == false)
                            PrintRealTimeEventTable();
                    }

                } while (Global_vars.cmd_finish == false);

                do
                {
                    status = AIS_ReadLog(handle[i], out log_index, out log_action, out log_reader_id, out log_card_id, out log_system_id, nfc_uid, out nfc_uid_len, out timestamp);

                    if (status > 0)
                    {
                        break;
                    }

                    if (Global_vars.gui_on == false)
                    {
                        Console.Write("     {0,-15}", log_index);
                        Console.Write("{0,-32}", Enum.GetName(typeof(CARD_ACTION), log_action));
                        Console.Write("{0,-15}", log_reader_id);
                        Console.Write("{0,-10}", log_card_id);
                        Console.Write("{0,-8}", log_system_id);
                        Console.Write("{0,-22}", BitConverter.ToString(nfc_uid));
                        Console.Write("  {0,-20}", nfc_uid_len);
                        Console.Write("{0,-30}", timestamp);
                        Console.WriteLine("{0,-5}", "L");
                    }
                    else
                    {
                        Console.WriteLine("RTE SENT");
                        Console.WriteLine("log index:{0}", log_index.ToString());
                        Console.WriteLine("log action:{0}", Enum.GetName(typeof(CARD_ACTION), log_action));
                        Console.WriteLine("log reader id:{0}", log_reader_id.ToString());
                        Console.WriteLine("log card id:{0}", log_card_id.ToString());
                        Console.WriteLine("log system id:{0}", log_system_id.ToString());
                        Console.WriteLine("nfc uid:{0}", BitConverter.ToString(nfc_uid));
                        Console.WriteLine("nfc uid len:{0}", nfc_uid_len.ToString());
                        Console.WriteLine("timetamp:{0}", timestamp.ToString());
                        Console.WriteLine("log type:{0}", "L");
                        Console.WriteLine("RTE READ");
                    }

                    File.AppendAllText(Global_vars.debug_file_path,
                           log_index + "           |" + Enum.GetName(typeof(CARD_ACTION), log_action) + "|    " +
                           log_reader_id + "       | "
                           + log_card_id + "     |    "
                           + log_system_id + "     |    "
                           + BitConverter.ToString(nfc_uid) + "                |    "
                           + nfc_uid_len + "       |"
                           + timestamp + "  | " + "     L" + Environment.NewLine);

                } while (true);
            }
        }
                
        public static void SendAllLogs(UIntPtr[] handle, ref Globals Global_vars)
        {
            
            DL_STATUS status = 0;
            DL_STATUS status_last = 0;

            string Contents = "";

            for (int i = 0; i < Global_vars.devices; i++)
            {

                Contents += "[";

                uint log_index = 0, log_action = 0, log_reader_id = 0, log_card_id = 0, log_system_id = 0, nfc_uid_len = 0;
                DateTime timestamp;
                byte[] nfc_uid = new byte[10];
                uint RealTimeEvents = 0, LogAvailable = 0, LogUnread = 0, cmdResponses = 0, cmdPercent = 0, DeviceStatus = 0, TimeoutOccurred = 0, int_status = 0;

                Global_vars.cmd_finish = false;

                AIS_ClearLog(handle[i]);

                status = AIS_GetLog(handle[i], Global_vars.password);
                if (status > 0)
                {
                    return;
                }
                File.AppendAllText(Global_vars.debug_file_path, "|||Sending logs to database...");
                do
                {
                    status = MainLoop(handle[i], out RealTimeEvents, out LogAvailable, out LogUnread, out cmdResponses, out cmdPercent, out DeviceStatus, out TimeoutOccurred, out int_status);

                    Console.Write("\rDevice[");
                    Console.Write(Global_vars.device_IDs[i]);
                    Console.Write("] ");
                    Console.Write("Sending Logs ... ");
                    Console.Write(cmdPercent);

                    

                    if (status > 0)
                    {
                        if (status_last != status)
                        {
                            status_last = status;
                        }

                        return;
                    }

                    if (cmdResponses > 0)
                    {
                        Global_vars.cmd_finish = true;
                      
                    }


                } while (Global_vars.cmd_finish == false);

                do
                {
                    status = AIS_ReadLog(handle[i], out log_index, out log_action, out log_reader_id, out log_card_id, out log_system_id, nfc_uid, out nfc_uid_len, out timestamp);

                    if (status > 0)
                    {
                        break;
                    }
               

                Contents += "{\"logindex" + '"' + ':' + '"' + log_index.ToString() + "\"," + '"'
                             + "logaction" + '"' + ":" + '"' + Enum.GetName(typeof(CARD_ACTION), log_action) + "\","
                       + '"' + "readerid" + '"' + ":" + '"' + log_reader_id.ToString() + "\","
                       + '"' + "cardid" + '"' + ":" + '"' + log_card_id.ToString() + "\","
                       + '"' + "systemid" + '"' + ":" + '"' + log_system_id.ToString() + "\","
                       + '"' + "nfcuid" + '"' + ":" + '"' + BitConverter.ToString(nfc_uid) + "\","
                       + '"' + "uidlength" + '"' + ":" + '"' + nfc_uid_len.ToString() + "\","
                       + '"' + "timestamp" + '"' + ":" + '"' + timestamp.ToString() + "\"},";

                } while (true);

                Contents = Contents.Remove(Contents.Length - 1);

                Contents += "]";

                HttpWebRequest request = (HttpWebRequest)

                WebRequest.Create("http://"+Global_vars.server_address+"/AisReaders/addlogs.php"); request.KeepAlive = false;

                request.ProtocolVersion = HttpVersion.Version10;

                request.Method = "POST";

                request.ContentType = "application/json; charset=UTF-8";

                request.ContentLength = Contents.Length;
                
                Stream requestStream = request.GetRequestStream();

                byte[] bytes = Encoding.ASCII.GetBytes(Contents);

                    // now sending it
                requestStream.Write(bytes, 0, Contents.Length);

                requestStream.Close();
                    // getting response and printing it out to the console along with the status code

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                string result;

                using (StreamReader rdr = new StreamReader(response.GetResponseStream()))
                {
                    result = rdr.ReadToEnd();
                }

                Console.WriteLine("");

            }
        }

        public static void WhitelistFromDB(ref Globals Global_vars)
        {
            DL_STATUS status = 0;
            
            File.AppendAllText(Global_vars.debug_file_path, "Reading whitelist from DB: " + Environment.NewLine);

            for (int i = 0; i < Global_vars.devices; i++)
            {
                string reader_wl = "";

                Console.Write("Reader number [");
                Console.Write(Global_vars.device_IDs[i]);
                Console.Write("] whitelist in database: ");

                reader_wl = ReadWhitelistFromDB(ref Global_vars.device_IDs[i], ref Global_vars);
                Console.WriteLine(reader_wl);
                
                File.AppendAllText(Global_vars.debug_file_path, "Reader number [" + Global_vars.device_IDs[i] + "] whitelist in database: " + reader_wl + Environment.NewLine);

                do
                {
                    AIS_Whitelist_Write(Global_vars.handle[i], Global_vars.password, reader_wl);

                } while (status == DL_STATUS.TIMEOUT_ERROR);

                    if (status > 0)
                {
                    Console.Write("Error while trying to write whitelist into reader ID [");
                    Console.Write(Global_vars.device_IDs[i]);
                    Console.WriteLine("]");

                    File.AppendAllText(Global_vars.debug_file_path, "Error while writing whitelist into reader ID [" + Global_vars.device_IDs[i] + "], status: " + status.ToString() + Environment.NewLine);
                    
                }
                else
                {
                    Console.Write("Whitelist successfully written into reader number [");
                    Console.Write(Global_vars.device_IDs[i]);
                    Console.WriteLine("]");

                    File.AppendAllText(Global_vars.debug_file_path, "Whitelist succesfully written into reader ID [" + Global_vars.device_IDs[i] + "], status: " + status.ToString() + Environment.NewLine);

                }
            }
        }

        public static string ReadWhitelistFromDB(ref uint device_id, ref Globals Global_vars)
        {

            string to_send = "id=" + device_id.ToString();
            string HtmlResult = "";
            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                HtmlResult = wc.UploadString("http://" + Global_vars.server_address + "/AisReaders/readwhitelistdb.php", to_send);
            }

            return HtmlResult;          
        }

        public static void BlacklistFromDB(ref Globals Global_vars)
        {
            DL_STATUS status = 0;
            
            File.AppendAllText(Global_vars.debug_file_path, "Reading blacklist from DB: " + Environment.NewLine);

            for (int i = 0; i < Global_vars.devices; i++)
            {
                string reader_bl = "";

                Console.Write("Reader number [");
                Console.Write(Global_vars.device_IDs[i]);
                Console.Write("] blacklist in database: ");
                reader_bl = ReadBlacklistFromDB(ref Global_vars.device_IDs[i], ref Global_vars);
                Console.WriteLine(reader_bl);
                              
                File.AppendAllText(Global_vars.debug_file_path, "Reader number [" + Global_vars.device_IDs[i] + "] blacklist in database: " + reader_bl + Environment.NewLine);

                
                do
                {
                    AIS_Blacklist_Write(Global_vars.handle[i], Global_vars.password, reader_bl);

                } while (status == DL_STATUS.TIMEOUT_ERROR);

                if (status > 0)
                {
                    Console.Write("Error while trying to write blacklist into reader number [");
                    Console.Write(Global_vars.device_IDs[i]);
                    Console.WriteLine("]");

                    File.AppendAllText(Global_vars.debug_file_path, "Error while writing blacklist into reader ID [" + Global_vars.device_IDs[i] + "], status: " + status.ToString() + Environment.NewLine);
                    
                }
                else
                {
                    Console.Write("Blacklist successfully written into reader number [");
                    Console.Write(Global_vars.device_IDs[i]);
                    Console.WriteLine("]");

                    File.AppendAllText(Global_vars.debug_file_path, "Blacklsit succesfully written into reader ID [" + Global_vars.device_IDs[i] + "], status: " + status.ToString() + Environment.NewLine);

                }

            }
        }
        public static string ReadBlacklistFromDB(ref uint device_id, ref Globals Global_vars)
        {

            string to_send = "id=" + device_id.ToString();
            string HtmlResult = "";
            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

                HtmlResult = wc.UploadString("http://" + Global_vars.server_address + "/AisReaders/readblacklistdb.php", to_send);
            }
            
            return HtmlResult;
            
        }

        public static void WhitelistFromReader(ref Globals Global_vars)
        {
            int ID = 0;
            if (Global_vars.gui_on == false)
            {
                
            Console.WriteLine("Enter device ID: ");
            ID = Convert.ToInt32(Console.ReadLine()); 
            Console.Write("Device ID [");
            Console.Write(ID);
            Console.Write("] whitelist: ");

            Console.WriteLine(ReadWhitelistFromReader(Global_vars.id_handle[ID], ref Global_vars));
            

            } else if (Global_vars.gui_on == true)
            {
                for (int i = 0; i < Global_vars.devices; i++)
                {
                    Console.WriteLine("Whitelist for device ID [" + Global_vars.device_IDs[i] + "] is:");
                    Console.WriteLine(ReadWhitelistFromReader(Global_vars.handle[i], ref Global_vars));

                }
            }


        }

        public static string ReadWhitelistFromReader(HND_AIS handle, ref Globals Global_vars)
        {
            string reader_wl = "";

            DL_STATUS status = 0;
          
            byte[] nfc_uid = new byte[10];

            uint RealTimeEvents = 0, LogAvailable = 0, LogUnread = 0, cmdResponses = 0, cmdPercent = 0, DeviceStatus = 0, TimeoutOccurred = 0, int_status = 0;

            do
            {
                MainLoop(handle, out RealTimeEvents, out LogAvailable, out LogUnread, out cmdResponses, out cmdPercent, out DeviceStatus, out TimeoutOccurred, out int_status);

                status = AIS_Whitelist_Read(handle, Global_vars.password, out reader_wl);

            } while (status == DL_STATUS.TIMEOUT_ERROR);

                if (status != DL_STATUS.DL_OK && status != DL_STATUS.ERR_BUFFER_EMPTY)
            {
                Console.Write("Error while reading whitelist from this device, status is: ");
                Console.WriteLine((Enum.GetName(typeof(DL_STATUS), status)));

                File.AppendAllText(Global_vars.debug_file_path, "Error while reading whitelist from this device, status: " + status.ToString() + Environment.NewLine);

            }

            return reader_wl;

        }
        public static void BlacklistFromReader(ref Globals Global_vars)
        {
            int ID = 0;

            if (Global_vars.gui_on == false)
            { 
                Console.WriteLine("Enter device ID: ");
            ID = Convert.ToInt32(Console.ReadLine());
            Console.Write("Device ID [");
            Console.Write(ID);
            Console.Write("] blacklist: ");

            Console.WriteLine(ReadBlacklistFromReader(Global_vars.id_handle[ID], ref Global_vars));
            }
            else if (Global_vars.gui_on == true)
            {
                for (int i = 0; i < Global_vars.devices; i++)
                {
                    Console.WriteLine("Blacklist for device ID [" + Global_vars.device_IDs[i] + "] is:");
                    Console.WriteLine(ReadBlacklistFromReader(Global_vars.handle[i], ref Global_vars));
                }
            }
           

        }

        public static string ReadBlacklistFromReader(HND_AIS handle, ref Globals Global_vars)
        {
            string reader_bl = "";
            DL_STATUS status = 0;
            
            byte[] nfc_uid = new byte[10];
            uint RealTimeEvents = 0, LogAvailable = 0, LogUnread = 0, cmdResponses = 0, cmdPercent = 0, DeviceStatus = 0, TimeoutOccurred = 0, int_status = 0;

            do
            {
                MainLoop(handle, out RealTimeEvents, out LogAvailable, out LogUnread, out cmdResponses, out cmdPercent, out DeviceStatus, out TimeoutOccurred, out int_status);

                status = AIS_Blacklist_Read(handle, Global_vars.password ,out reader_bl);
            } while (status == DL_STATUS.TIMEOUT_ERROR);

            if (status != DL_STATUS.DL_OK && status != DL_STATUS.ERR_BUFFER_EMPTY)
            {
                Console.Write("Error while reading blacklist from this device, status is: ");
                Console.WriteLine((Enum.GetName(typeof(DL_STATUS), status)));

                File.AppendAllText(Global_vars.debug_file_path, "Error while reading blacklist from this device, status: " + status.ToString() + Environment.NewLine);

            }

            return reader_bl;
        }


        public static void GetDevicesTime(HND_AIS handle, uint device_id)
        {
            Globals Global_vars = new Globals();
            DateTime get_time;
            int timezone, DST, offset;
            int additional = 0;
            DL_STATUS status = 0;

            status = AIS_GetTime(handle, out get_time, out timezone, out DST, out offset, additional);

            if (status > 0)
            {
                Console.Write("Error while trying to get time from device number: ");
                Console.Write(device_id);
                Console.Write(" status is: ");
                Console.WriteLine((Enum.GetName(typeof(DL_STATUS), status)));

                File.AppendAllText(Global_vars.debug_file_path, "Eror while trying to get time from device number: " + device_id + "  status: " + status.ToString() + Environment.NewLine);

            }
            else
            {
                Console.Write(Environment.NewLine + "Device number: ");
                Console.Write(device_id);
                Console.Write(" time is: ");
                Console.WriteLine(get_time);

                File.AppendAllText(Global_vars.debug_file_path, "Device number: " + device_id + " time is: " + get_time + " status: " + status.ToString() + Environment.NewLine);

            }

          }
        

   
        public static DateTime TimeFromInternet()
        {
            DateTime dateTime = DateTime.MinValue;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://nist.time.gov/actualtime.cgi?lzbc=siqm9b");

            request.Method = "GET";

            request.Accept = "text/html, application/xhtml+xml, */*";

            request.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/6.0)";

            request.ContentType = "application/x-www-form-urlencoded";

            request.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore); //No caching

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                StreamReader stream = new StreamReader(response.GetResponseStream());

                string html = stream.ReadToEnd();

                string time = System.Text.RegularExpressions.Regex.Match(html, @"(?<=\btime="")[^""]*").Value;

                double milliseconds = Convert.ToInt64(time) / 1000.0;

                dateTime = new DateTime(1970, 1, 1).AddMilliseconds(milliseconds).ToLocalTime();
            }

            return dateTime;
        }

        public static void SetDevicesTime(HND_AIS handle, uint device_id, ref Globals Global_vars)
        {
            DateTime current_time = DateTime.Now;
            TimeSpan diff = TimeSpan.Zero;
            int timezone = 0;
            int DST = 0;
            int offset = 0;
            int additional = 0;
            DL_STATUS status = 0;


            if (Global_vars.InternetOrComputer == 1)
            {
                DateTime internet_time = TimeFromInternet();

                status = AIS_GetTime(handle, out current_time, out timezone, out DST, out offset, additional);
                if (status > 0)
                {
                    Console.Write("Error while trying to get time from device number: ");
                    Console.Write(device_id);
                    Console.Write(" status is: ");
                    Console.WriteLine((Enum.GetName(typeof(DL_STATUS), status)));

                    File.AppendAllText(Global_vars.debug_file_path, "Error while trying to get time from device number: " + device_id +  " status is: " + status.ToString() + Environment.NewLine);

                }
                else
                {
                    diff = internet_time.Subtract(current_time);

                    if (diff.TotalSeconds > 10 || diff.TotalSeconds < 10)
                    {
                        AIS_SetTime(handle, Global_vars.password, internet_time, 0, Global_vars.DaylightSavingTime, 0, 0);
                        if (status > 0)
                        {
                            Console.Write("Error while trying to set time to device number: ");
                            Console.Write(device_id);
                            Console.Write(" status is: ");
                            Console.WriteLine((Enum.GetName(typeof(DL_STATUS), status)));

                            File.AppendAllText(Global_vars.debug_file_path, "Error while trying to set time to device number: " + device_id + " status is: " + status.ToString() + Environment.NewLine);

                        }
                        else
                        {
                            Console.Write("Time succesfully set to device number: ");
                            Console.Write(device_id);
                            Console.Write(" status is: ");
                            Console.WriteLine((Enum.GetName(typeof(DL_STATUS), status)));

                            File.AppendAllText(Global_vars.debug_file_path, "Time succesfully set to device number: " + device_id + " time set: " + internet_time + " status is: " + status.ToString() + Environment.NewLine);
                            
                        }
                    }
                }

            }

            else if (Global_vars.InternetOrComputer == 2)
            {
                DateTime pc_time = DateTime.Now;
                
                status = AIS_GetTime(handle, out current_time, out timezone, out DST, out offset, additional);

                if (status > 0)
                {
                    Console.Write("Error while trying to get time from device number: ");
                    Console.Write(device_id);
                    Console.Write(" status is: ");
                    Console.WriteLine((Enum.GetName(typeof(DL_STATUS), status)));

                    File.AppendAllText(Global_vars.debug_file_path, "Error while trying to get time from device number: " + device_id + " status is: " + status.ToString() + Environment.NewLine);

                }
                else
                {
                    diff = pc_time.Subtract(current_time);
                    if (diff.TotalSeconds > 10 || diff.TotalSeconds < 10)
                    {
                        AIS_SetTime(handle, Global_vars.password, pc_time, 0, Global_vars.DaylightSavingTime, 0, 0);

                        if (status > 0)
                        {
                            Console.Write("Error while trying to set time to device number: ");
                            Console.Write(device_id);
                            Console.Write(" status is: ");
                            Console.WriteLine((Enum.GetName(typeof(DL_STATUS), status)));;

                            File.AppendAllText(Global_vars.debug_file_path, "Error while trying to set time from device number: " + device_id + " status is: " + status.ToString() + Environment.NewLine);

                        }
                        else
                        {
                            Console.Write("Time succesfully set to device number: ");
                            Console.Write(device_id);
                            Console.Write(" status is: ");
                            Console.WriteLine((Enum.GetName(typeof(DL_STATUS), status)));

                            File.AppendAllText(Global_vars.debug_file_path, "Time succesfully set to device number: " + device_id + " time set: " + pc_time + " status is: " + status.ToString() + Environment.NewLine);

                        }
                    }                        
                }
            }
        }

        public static void SetDevicesTimeManual(HND_AIS handle, uint device_id, ref Globals Global_vars)
        {
          // default time from PC to set, or based on settings.ini internet/PC choices?
            DateTime current_time = DateTime.Now;
            DateTime get_time;
            int timezone, DST, offset;
            int additional = 0;
            DL_STATUS status = 0;            

            status = AIS_GetTime(handle, out get_time, out timezone, out DST, out offset, additional);

            status = AIS_SetTime(handle, Global_vars.password , current_time, timezone, DST, offset, additional);

            if (status == DL_STATUS.DL_OK)

                Console.Write("Device ID [");
                Console.Write(device_id);
                Console.Write("] current time set: ");
                Console.WriteLine(current_time);

                status = AIS_GetTime(handle, out get_time, out timezone, out DST, out offset, additional);

            File.AppendAllText(Global_vars.debug_file_path, "Current time succesfully set to device number: " + device_id + " time from PC set: " + current_time + " status is: " + status.ToString() + Environment.NewLine);
        }

        public static void LogsByIndex(UIntPtr[] handle, ref Globals Global_vars)
        {

            File.AppendAllText(Global_vars.debug_file_path, "Get logs by index:" + Environment.NewLine);

            uint start_index = 0 ;
            uint end_index = 0;
            if (Global_vars.gui_on == false)
                Console.WriteLine("Enter start index:");
            start_index = Convert.ToUInt32(Console.ReadLine());

            File.AppendAllText(Global_vars.debug_file_path, "start index = " + start_index + Environment.NewLine);

            if (Global_vars.gui_on == false)
                Console.WriteLine("Enter end index:");
            end_index = Convert.ToUInt32(Console.ReadLine());

            File.AppendAllText(Global_vars.debug_file_path, "end index = " + end_index + Environment.NewLine);

            DL_STATUS status = 0;
            DL_STATUS status_last = 0;

            for (int i = 0; i < Global_vars.devices; i++)
            {

                uint log_index = 0, log_action = 0, log_reader_id = 0, log_card_id = 0, log_system_id = 0, nfc_uid_len = 0;
                DateTime timestamp;
                byte[] nfc_uid = new byte[7];
                uint RealTimeEvents = 0, LogAvailable = 0, LogUnread = 0, cmdResponses = 0, cmdPercent = 0, DeviceStatus = 0, TimeoutOccurred = 0, int_status = 0;

                Global_vars.cmd_finish = false;

                AIS_ClearLog(handle[i]);

                status = AIS_GetLogByIndex(handle[i], Global_vars.password, start_index,end_index);

                if (status > 0)
                {
                    return;
                }
                File.AppendAllText(Global_vars.debug_file_path, "Downloading logs ...");
                do
                {
                    status = MainLoop(handle[i], out RealTimeEvents, out LogAvailable, out LogUnread, out cmdResponses, out cmdPercent, out DeviceStatus, out TimeoutOccurred, out int_status);

                    Console.Write("\rDownloading Logs ... ");
                    Console.Write(cmdPercent);

                    if (status > 0)
                    {
                        if (status_last != status)
                        {
                            status_last = status;
                        }

                        return;
                    }

                    if (cmdResponses > 0)
                    {
                        Global_vars.cmd_finish = true;
                        
                        Console.WriteLine("");
                        Console.Write("Device ID[");
                        Console.Write(Global_vars.device_IDs[i]);
                        Console.WriteLine("] logs by index:");
                        
                        File.AppendAllText(Global_vars.debug_file_path, "Device ID[" + Global_vars.device_IDs[i] + "] logs:");
                        
                    }
                    
                } while (Global_vars.cmd_finish == false);

                do
                {
                    status = AIS_ReadLog(handle[i], out log_index, out log_action, out log_reader_id, out log_card_id, out log_system_id, nfc_uid, out nfc_uid_len, out timestamp);

                    if (status > 0)
                    {
                        break;
                    }

                    if (Global_vars.gui_on == false)
                    {
                        Console.Write("     {0,-15}", log_index);
                        Console.Write("{0,-32}", Enum.GetName(typeof(CARD_ACTION), log_action));
                        Console.Write("{0,-15}", log_reader_id);
                        Console.Write("{0,-10}", log_card_id);
                        Console.Write("{0,-8}", log_system_id);
                        Console.Write("{0,-22}", BitConverter.ToString(nfc_uid));
                        Console.Write("  {0,-20}", nfc_uid_len);
                        Console.Write("{0,-30}", timestamp);
                        Console.WriteLine("{0,-5}", "L");
                    }
                    else
                    {
                        Console.WriteLine("RTE SENT");
                        Console.WriteLine("log index:{0}", log_index.ToString());
                        Console.WriteLine("log action:{0}", Enum.GetName(typeof(CARD_ACTION), log_action));
                        Console.WriteLine("log reader id:{0}", log_reader_id.ToString());
                        Console.WriteLine("log card id:{0}", log_card_id.ToString());
                        Console.WriteLine("log system id:{0}", log_system_id.ToString());
                        Console.WriteLine("nfc uid:{0}", BitConverter.ToString(nfc_uid));
                        Console.WriteLine("nfc uid len:{0}", nfc_uid_len.ToString());
                        Console.WriteLine("timetamp:{0}", timestamp.ToString());
                        Console.WriteLine("log type:{0}", "L");
                        Console.WriteLine("RTE READ");
                    }

                    File.AppendAllText(Global_vars.debug_file_path,
                          log_index + "           |" + Enum.GetName(typeof(CARD_ACTION), log_action) + "|    " +
                          log_reader_id + "       | "
                          + log_card_id + "     |    "
                          + log_system_id + "     |    "
                          + BitConverter.ToString(nfc_uid) + "                |    "
                          + nfc_uid_len + "       |"
                          + timestamp + "  | " + "     L" + Environment.NewLine);

                } while (true);

            }
        }

        public static void LogsByTime(UIntPtr[] handle, ref Globals Global_vars)
        {
            if (Global_vars.gui_on == false)
                Console.WriteLine("Enter starting date and time from which to read logs, format: hh:mm:ss dd.mm.yyyy");
            var start_time = StringToTimestamp(Console.ReadLine());
            if (Global_vars.gui_on == false)
                Console.WriteLine("Enter end date and time for reading logs, format: hh:mm:ss dd.mm.yyyy");
            var end_time = StringToTimestamp(Console.ReadLine());
          
            DL_STATUS status = 0;

            for (int i = 0; i < Global_vars.devices; i++)
            {

                uint log_index = 0, log_action = 0, log_reader_id = 0, log_card_id = 0, log_system_id = 0, nfc_uid_len = 0;
                DateTime timestamp;
                byte[] nfc_uid = new byte[7];
                uint RealTimeEvents = 0, LogAvailable = 0, LogUnread = 0, cmdResponses = 0, cmdPercent = 0, DeviceStatus = 0, TimeoutOccurred = 0, int_status = 0;

                Global_vars.cmd_finish = false;

                AIS_ClearLog(handle[i]);
                
                status = AIS_GetLogByTime(handle[i], Global_vars.password, start_time, end_time);
                if (status > 0)
                {
                    return;
                }

                do
                {
                    status = MainLoop(handle[i], out RealTimeEvents, out LogAvailable, out LogUnread, out cmdResponses, out cmdPercent, out DeviceStatus, out TimeoutOccurred, out int_status);

                    Console.Write("\rDownloading Logs ... ");
                    Console.Write(cmdPercent);

                    if (status > 0)
                    {
                        return;
                    }

                    if (cmdResponses > 0)
                    {
                        Global_vars.cmd_finish = true;
                        Console.WriteLine("");
                        Console.Write("Device[");
                        Console.Write(Global_vars.device_IDs[i]);
                        Console.WriteLine("] logs by time:");

                        File.AppendAllText(Global_vars.debug_file_path, "Device ID[" + Global_vars.device_IDs[i] + "] logs:");                        
                    }
                    
                } while (Global_vars.cmd_finish == false);

                do
                {
                    status = AIS_ReadLog(handle[i], out log_index, out log_action, out log_reader_id, out log_card_id, out log_system_id, nfc_uid, out nfc_uid_len, out timestamp);

                    if (status > 0)
                    {
                        break;
                    }
                    if (Global_vars.gui_on == false)
                    {
                        Console.Write("     {0,-15}", log_index);
                        Console.Write("{0,-32}", Enum.GetName(typeof(CARD_ACTION), log_action));
                        Console.Write("{0,-15}", log_reader_id);
                        Console.Write("{0,-10}", log_card_id);
                        Console.Write("{0,-8}", log_system_id);
                        Console.Write("{0,-22}", BitConverter.ToString(nfc_uid));
                        Console.Write("  {0,-20}", nfc_uid_len);
                        Console.Write("{0,-30}", timestamp);
                        Console.WriteLine("{0,-5}", "L");
                    }
                    else
                    {
                        Console.WriteLine("RTE SENT");
                        Console.WriteLine("log index:{0}", log_index.ToString());
                        Console.WriteLine("log action:{0}", Enum.GetName(typeof(CARD_ACTION), log_action));
                        Console.WriteLine("log reader id:{0}", log_reader_id.ToString());
                        Console.WriteLine("log card id:{0}", log_card_id.ToString());
                        Console.WriteLine("log system id:{0}", log_system_id.ToString());
                        Console.WriteLine("nfc uid:{0}", BitConverter.ToString(nfc_uid));
                        Console.WriteLine("nfc uid len:{0}", nfc_uid_len.ToString());
                        Console.WriteLine("timetamp:{0}", timestamp.ToString());
                        Console.WriteLine("log type:{0}", "L");
                        Console.WriteLine("RTE READ");
                    }

                    File.AppendAllText(Global_vars.debug_file_path,
                          log_index + "           |" + Enum.GetName(typeof(CARD_ACTION), log_action) + "|    " +
                          log_reader_id + "       | "
                          + log_card_id + "     |    "
                          + log_system_id + "     |    "
                          + BitConverter.ToString(nfc_uid) + "                |    "
                          + nfc_uid_len + "       |"
                          + timestamp + "  | " + "     L" + Environment.NewLine);

                } while (true);

            }
        }


        public static void menu(ConsoleKey key, ref Globals Global_vars)
        {
          
            switch (key)
            {
                case ConsoleKey.D1:
                        Console.WriteLine("Read and write whitelist from database: ");                   
                        WhitelistFromDB(ref Global_vars);
                            PrintRealTimeEventTable();
                    break;

                case ConsoleKey.D2:
                        Console.WriteLine("Read and write blacklist from database: ");
                        BlacklistFromDB(ref Global_vars);
                        PrintRealTimeEventTable();
                    break;

                case ConsoleKey.D3:
                        GetLogs(Global_vars.handle, ref Global_vars);
                        PrintRealTimeEventTable();
                    break;

                case ConsoleKey.D4:
                    Console.WriteLine("Read whitelist from reader: ");
                    WhitelistFromReader(ref Global_vars);
                        PrintRealTimeEventTable();
                    break;

                case ConsoleKey.D5:
                    Console.WriteLine("Read blacklist from reader: ");
                    BlacklistFromReader(ref Global_vars);
                        PrintRealTimeEventTable(); ;
                    break;

                case ConsoleKey.D6:
                        SendAllLogs(Global_vars.handle, ref Global_vars);
                        PrintRealTimeEventTable();
                    break;

                case ConsoleKey.D7:
                        for (int i = 0; i < Global_vars.devices; i++)
                        {
                            SetDevicesTimeManual(Global_vars.handle[i], Global_vars.device_IDs[i],ref Global_vars);
                        }
                        PrintRealTimeEventTable();
                    break;

                case ConsoleKey.D8:
                        for (int i = 0; i < Global_vars.devices; i++)
                        {
                        GetDevicesTime(Global_vars.handle[i], Global_vars.device_IDs[i]);
                        }
                        PrintRealTimeEventTable();
                    break;
                    
                case ConsoleKey.I:
                    Console.WriteLine("Get logs by index: ");
                    LogsByIndex(Global_vars.handle, ref Global_vars);
                        PrintRealTimeEventTable();
                    break;

                case ConsoleKey.T:
                    Console.WriteLine("Get logs by time: ");
                    LogsByTime(Global_vars.handle, ref Global_vars);
                        PrintRealTimeEventTable();
                    break;
                default:
                    print_menu();
                    break;
            }
        }

        public static void gui_menu(string command, ref Globals Global_vars)
        {

            switch (command)
            {                
                case "1":
                    Console.WriteLine("Read and write whitelist from database: ");
                    WhitelistFromDB(ref Global_vars);
                    break;

                case "2":
                    Console.WriteLine("Read and write blacklist from database: ");
                    BlacklistFromDB(ref Global_vars);
                    break;

                case "3":
                    GetLogs(Global_vars.handle, ref Global_vars);
                    break;

                case "4":
                    Console.WriteLine("Read whitelist from reader: ");
                    WhitelistFromReader(ref Global_vars);
                    break;

                case "5":
                    Console.WriteLine("Read blacklist from reader: ");
                    BlacklistFromReader(ref Global_vars);
                    break;

                case "6":
                    SendAllLogs(Global_vars.handle, ref Global_vars);
                    break;

                case  "7":
                    for (int i = 0; i < Global_vars.devices; i++)
                    {
                        SetDevicesTimeManual(Global_vars.handle[i], Global_vars.device_IDs[i], ref Global_vars);
                    }
                    break;

                case "8":
                    for (int i = 0; i < Global_vars.devices; i++)
                    {
                        GetDevicesTime(Global_vars.handle[i], Global_vars.device_IDs[i]);
                    }
                    break;

                case "I":
                    Console.WriteLine("Get logs by index:");
                    LogsByIndex(Global_vars.handle, ref Global_vars);
                    Console.WriteLine("Logs done");
                    break;
                    // suspend datagrid while writing Logs ; 
                case "T":
                    Console.WriteLine("Get logs by time:");
                    LogsByTime(Global_vars.handle, ref Global_vars);
                    Console.WriteLine("Logs done");
                    break;

                default:
                    print_menu();
                    break;
            }
        }
        
        public static void ReadSettings(ref Globals Global_vars)
        {
            string line = "";            
            int i = 0;

            try

            {
                string settings_path = "settings.ini";
                if (Global_vars.gui_on)
                    settings_path = "../../../" + settings_path;

            StreamReader file = new StreamReader(settings_path);

             while ((line = file.ReadLine()) != null)
            {
                Global_vars.settings[i] = line;
                i++;
            }

            file.Close();

            int pos = 0;
            
                pos = Global_vars.settings[0].IndexOf('=');
                Global_vars.str_time_to_send = Global_vars.settings[0].Substring(pos + 2);

                pos = Global_vars.settings[1].IndexOf('=');
                Global_vars.str_minutes_check = Global_vars.settings[1].Substring(pos + 2);

                pos = Global_vars.settings[2].IndexOf('=');
                Global_vars.server_address = Global_vars.settings[2].Substring(pos + 2);

                pos = Global_vars.settings[3].IndexOf('=');
                Global_vars.send_hours = Convert.ToInt32(Global_vars.settings[3].Substring(pos + 2));

                pos = Global_vars.settings[4].IndexOf('=');
                Global_vars.password = Global_vars.settings[4].Substring(pos + 2);

                pos = Global_vars.settings[5].IndexOf('=');
                Global_vars.InternetOrComputer = Convert.ToInt32(Global_vars.settings[5].Substring(pos + 2));

                pos = Global_vars.settings[6].IndexOf('=');
                Global_vars.DaylightSavingTime = Convert.ToInt32(Global_vars.settings[6].Substring(pos + 2));
                
            }
            catch (Exception e)
            {
                Console.WriteLine("unable to check settings.ini");
                Console.WriteLine(e);
            }
        }

        public static DateTime StringToTimestamp(string datetime)
        {
            if (datetime.Length < 19)
           {
                Console.WriteLine("invalid input - can't convert to timestamp, format must be hh:mm:ss dd.mm.yyyy");                
           }

            DateTime converted;

            int hour = Convert.ToInt32(datetime.Substring(0, 2));
            int min = Convert.ToInt32(datetime.Substring(3, 2));
            int sec = Convert.ToInt32(datetime.Substring(6, 2));
            int day = Convert.ToInt32(datetime.Substring(9, 2));
            int mon = Convert.ToInt32(datetime.Substring(12, 2));
            int year = Convert.ToInt32(datetime.Substring(15, 4));

            converted = new DateTime(year, mon, day, hour, min, sec);

            return converted;

        }

    }
}
