

using System;
using System.Runtime.InteropServices;






namespace uFrAdvance
{
    
    using DL_STATUS = System.UInt32;

    public enum device_e
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
                   //--------------------
        DL_AIS_BMR, // Barcode NFC Reader, Half-Duplex

        DL_AIS_BASE_HD, // Base HD AIS old, Half-Duplex

        DL_AIS_BASE_HD_SDK, // Base HD AIS SDK, Half-Duplex

        DL_AIS_IO_EXTENDER,
    }

    public enum UFR_STATUS
    {

        DL_OK,
        COMMUNICATION_ERROR = 0x01,
        CHKSUM_ERROR = 0x02,
        READING_ERROR = 0x03,
        WRITING_ERROR = 0x04,
        BUFFER_OVERFLOW = 0x05,
        MAX_ADDRESS_EXCEEDED = 0x06,
        MAX_KEY_INDEX_EXCEEDED = 0x07,
        NO_CARD = 0x08,
        COMMAND_NOT_SUPPORTED = 0x09,
        FORBIDEN_DIRECT_WRITE_IN_SECTOR_TRAILER = 0x0A,
        ADDRESSED_BLOCK_IS_NOT_SECTOR_TRAILER = 0x0B,
        WRONG_ADDRESS_MODE = 0x0C,
        WRONG_ACCESS_BITS_VALUES = 0x0D,
        AUTH_ERROR = 0x0E,
        PARAMETERS_ERROR = 0x0F,
        MAX_SIZE_EXCEEDED = 0x10,
        UNSUPPORTED_CARD_TYPE = 0x11,

        COMMUNICATION_BREAK = 0x50,
        NO_MEMORY_ERROR = 0x51,
        CAN_NOT_OPEN_READER = 0x52,
        READER_NOT_SUPPORTED = 0x53,
        READER_OPENING_ERROR = 0x54,
        READER_PORT_NOT_OPENED = 0x55,
        CANT_CLOSE_READER_PORT = 0x56,

        WRITE_VERIFICATION_ERROR = 0x70,
        BUFFER_SIZE_EXCEEDED = 0x71,
        VALUE_BLOCK_INVALID = 0x72,
        VALUE_BLOCK_ADDR_INVALID = 0x73,
        VALUE_BLOCK_MANIPULATION_ERROR = 0x74,
        WRONG_UI_MODE = 0x75,
        KEYS_LOCKED = 0x76,
        KEYS_UNLOCKED = 0x77,
        WRONG_PASSWORD = 0x78,
        CAN_NOT_LOCK_DEVICE = 0x79,
        CAN_NOT_UNLOCK_DEVICE = 0x7A,
        DEVICE_EEPROM_BUSY = 0x7B,
        RTC_SET_ERROR = 0x7C,
        ANTICOLLISION_DISABLED = 0x7D,
        NO_CARDS_ENUMERRATED = 0x7E,
        CARD_ALREADY_SELECTED = 0x7F,

        FT_STATUS_ERROR_1 = 0xA0,
        FT_STATUS_ERROR_2 = 0xA1,
        FT_STATUS_ERROR_3 = 0xA2,
        FT_STATUS_ERROR_4 = 0xA3,
        FT_STATUS_ERROR_5 = 0xA4,
        FT_STATUS_ERROR_6 = 0xA5,
        FT_STATUS_ERROR_7 = 0xA6,
        FT_STATUS_ERROR_8 = 0xA7,
        FT_STATUS_ERROR_9 = 0xA8,
        UFR_APDU_TRANSCEIVE_ERROR = 0xAE,
        UFR_APDU_JC_APP_NOT_SELECTED = 0x6000,
        UFR_APDU_JC_APP_BUFF_EMPTY,
        UFR_APDU_WRONG_SELECT_RESPONSE,
        UFR_APDU_WRONG_KEY_TYPE,
        UFR_APDU_WRONG_KEY_SIZE,
        UFR_APDU_WRONG_KEY_PARAMS,
        UFR_APDU_WRONG_ALGORITHM,
        UFR_APDU_PLAIN_TEXT_SIZE_EXCEEDED,
        UFR_APDU_UNSUPPORTED_KEY_SIZE,
        UFR_APDU_UNSUPPORTED_ALGORITHMS,
        UFR_APDU_RECORD_NOT_FOUND,
        UFR_APDU_SW_TAG = 0x0A0000

    }



    public unsafe class uFCoder
    {

#if WIN64

        const string DLL_PATH = "..\\..\\ufr-lib\\windows\\x86_64\\";
        const string NAME_DLL = "ais_readers-x86_64.dll";

#else
        const string DLL_PATH = "..\\..\\lib\\windows\\x86\\";

        const string NAME_DLL = "ais_readers-x86.dll";


#endif
        const string DLL_NAME = DLL_PATH + NAME_DLL;

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "ReaderOpen")]
        public static extern DL_STATUS ReaderOpen();

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "ReaderClose")]
        public static extern DL_STATUS ReaderClose();

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "ReaderReset")]
        public static extern DL_STATUS ReaderReset();

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "ReaderSoftRestart")]
        public static extern DL_STATUS ReaderSoftRestart();

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "GetReaderType")]
        public static extern DL_STATUS GetReaderType(ulong* get_reader_type);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "ReaderKeyWrite")]
        public static extern DL_STATUS ReaderKeyWrite(byte* aucKey, byte ucKeyIndex);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "GetReaderSerialNumber")]
        public static extern DL_STATUS GetReaderSerialNumber(ulong* serial_number);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "GetCardId")]
        public static extern DL_STATUS GetCardId(byte* card_type, ulong* card_serial);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "GetCardIdEx")]
        public static extern DL_STATUS GetCardIdEx(byte* bCardType,
                                                   byte* bCardUid,
                                                   byte* bUidSize);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "GetDlogicCardType")]
        public static extern DL_STATUS GetDlogicCardType(byte* bCardType);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "ReaderUISignal")]
        public static extern DL_STATUS ReaderUISignal(int light_mode, int sound_mode);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "ReadUserData")]
        public static extern DL_STATUS ReadUserData(byte* aucData);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "WriteUserData")]
        public static extern DL_STATUS WriteUserData(byte* aucData);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "GetReaderHardwareVersion")]
        public static extern DL_STATUS GetReaderHardwareVersion(byte* bVerMajor,
                                                                byte* bVerMinor);
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "GetReaderFirmwareVersion")]
        public static extern DL_STATUS GetReaderFirmwareVersion(byte* bVerMajor,
                                                               byte* bVerMinor);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "GetReaderSerialDescription")]
        public static extern DL_STATUS GetReaderSerialDescription(byte[] SerialDescription);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "GetBuildNumber")]
        public static extern DL_STATUS GetBuildNumber(byte* build);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "UFR_Status2String")]
        private static extern IntPtr UFR_Status2String(DL_STATUS status);
        public static string status2str(DL_STATUS status)
        {
            IntPtr str_ret = UFR_Status2String(status);
            return Marshal.PtrToStringAnsi(str_ret);
        }

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "GetDllVersionStr")]
        private static extern IntPtr GetDllVersionStr();
        public static string GetLibraryVersion()
        {
            IntPtr str_ret = GetDllVersionStr();
            return Marshal.PtrToStringAnsi(str_ret);
        }
        //==================================
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "AIS_MainLoop")]
        public static extern DL_STATUS AIS_MainLoop(int* RealTimeEvents, int* LogAvailable, int* LogUnread, int* cmdResponses, int* cmdPercent, int* DeviceStatus, int* TimeoutOccurred, int* status);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "AIS_ReadRTE_Count")]
        public static extern DL_STATUS AIS_ReadRTE_Count(IntPtr device);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "AIS_ReadRTE")]
        public static extern DL_STATUS AIS_ReadRTE(IntPtr device, int* log_index, int* log_action, int* log_reader_id, int* log_card_id, int* log_system_id, byte[] nfc_uid, UInt64* timestamp);


        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, EntryPoint = "AIS_List_AddDeviceForCheck")]
        public static extern DL_STATUS AIS_List_AddDeviceForCheck(UInt32 device_type, UInt32 device_id);




        


    }
    //---------------------------------------------------------------------------------------------------------------------------------



    
}

