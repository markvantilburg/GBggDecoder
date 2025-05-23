namespace GBggDecoder
{
    internal class GBRomHandler
    {
        public string GetTitle(byte[] romFile)
        {
            byte[] titleBytes = new byte[16];
            Array.Copy(romFile, 0x0134, titleBytes, 0, 16);
            return System.Text.Encoding.ASCII.GetString(titleBytes).TrimEnd('\0');
        }
    }
}