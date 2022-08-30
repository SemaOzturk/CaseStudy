// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using CampaignCode;

var codeGenerator = new CodeGenerator();

codeGenerator.GenerateCodes();
string code = "5X44XKX2";
Console.WriteLine($"{code} : isValid = {codeGenerator.CheckCode(code)}");