using System.CommandLine;

namespace CornelsenTools;

class Program
{
    private static async Task Main(string[] args)
    {
        var rootCommand = new RootCommand("A tool to decrypt Cornelsen files.");

        var fileOption = new Option<FileInfo?>(
            name: "--file",
            description: "The file to read and decrypt.");
        var outputOption = new Option<FileInfo?>(
            name: "--output",
            description: "The file to write the decrypted content to.");

        var decryptCommand = new Command("decrypt", "Decrypt a file.")
        {
            fileOption,
            outputOption
        };
        var encryptCommand = new Command("encrypt", "Decrypt a file.")
        {
            fileOption,
            outputOption
        };

        decryptCommand.SetHandler((fileOptionPath, outputOptionPath) =>
        {
            DoEncryptionDecryption(false, fileOptionPath, outputOptionPath);

            return Task.CompletedTask;
        }, fileOption, outputOption);
        encryptCommand.SetHandler((fileOptionPath, outputOptionPath) =>
        {
            DoEncryptionDecryption(true, fileOptionPath, outputOptionPath);

            return Task.CompletedTask;
        }, fileOption, outputOption);

        rootCommand.AddCommand(decryptCommand);
        rootCommand.AddCommand(encryptCommand);

        await rootCommand.InvokeAsync(args);
    }

    private static void DoEncryptionDecryption(bool encrypt, FileInfo? file, FileInfo? output)
    {
        if (file is null or { Exists: false })
        {
            Console.WriteLine("File doesn't exist!");
            return;
        }

        output ??= new FileInfo(file!.FullName + ".encrypted");

        var toEncrypt = File.ReadAllBytes(file!.FullName);
        var encrypted = encrypt ? EncryptionTools.Encrypt(toEncrypt) : EncryptionTools.Decrypt(toEncrypt);
        File.WriteAllBytes(output!.FullName, encrypted);

        Console.WriteLine($"{(encrypt ? "Encrypted" : "Decrypted")} the file successfully!");
    }
}