using GBggDecoder;

Console.WriteLine("Convert game boy Game Genie Code to rom location/patch data:");
Console.WriteLine("Enter Game Genie Code (e.g., 09D-49C-E62 or 6D8-36F):");
string code = Console.ReadLine();
if (GBGameGenieHandler.IsValidGBGGCode(code))
{
    var rawCode = GBGameGenieHandler.DecodeGBGG(code);
    var formatted = GBGameGenieHandler.FormatRawCode(rawCode);
    Console.WriteLine($"Address: {formatted.Address}, Value: {formatted.Value}");
    if (!string.IsNullOrEmpty(formatted.Compare))
    {
        Console.WriteLine($"Compare: {formatted.Compare}");
    }
}
else
{
    Console.WriteLine("Invalid Game Genie code.");
}


/*
 * Patch a rom using this information: (valid for rom with CRC32: 2C27EC70)
 *
   // Set the value at the specified address
   int address = 0x3D49;
   byte value = 9;
   byte compare = 2;

   // Ensure the address is within the bounds of the array
   if (address >= 0 && address < gameRom.Length)
   {
       if (gameRom[address] == compare)
       {
           gameRom[address] = value;
       }
   }
 * 
 */