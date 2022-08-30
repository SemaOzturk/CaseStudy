using System.Text.Json;

namespace CampaignCode;

public class CodeGenerator
{
    
string allowedCharacters = "ACDEFGHKLMNPRTXYZ234579";

Dictionary<char, short> allowedCharctersMap = new()
{
    {'A', 1},
    {'X', 2 },
    {'9', 3 },
    {'G', 4 },
    {'D', 5 },
    {'L', 6 },
    {'R', 7 },
    {'P', 8 },
    {'4', 9 },
    {'T', 10 },
    {'3', 11 },
     {'C', -1},
    {'E', -2 },
    {'F', -3 },
    {'H', -4 },
    {'K', -5 },
    {'M', -6 },
    {'N', -7 },
    {'5', -8 },
    {'Y', -9 },
    {'Z', -10 },
    {'2', -11 },
    {'7', -12 }
};

Dictionary<char, short> positiveCharactersMap = new()
{
    {'A', 1},
    {'X', 2 },
    {'9', 3 },
    {'G', 4 },
    {'D', 5 },
    {'L', 6 },
    {'R', 7 },
    {'P', 8 },
    {'4', 9 },
    {'T', 10 },
    {'3', 11 },
};

Dictionary<char, short> negativeCharactersMap = new()
{
    {'C', -1},
    {'E', -2 },
    {'F', -3 },
    {'H', -4 },
    {'K', -5 },
    {'M', -6 },
    {'N', -7 },
    {'5', -8 },
    {'Y', -9 },
    {'Z', -10 },
    {'2', -11 },
    {'7', -12 },
};



public void GenerateCodes()
{

    var codeList = new HashSet<string>();
    Random random = new Random();
    for (int codeIndex = 0; codeIndex < 10_000_000; codeIndex++)
    {
        string code = "";
        short codeNum = 0;
        if (random.Next(1, 3) == 1)
        {

            var posStartIndex = random.Next(0, positiveCharactersMap.Count);
            var posStartElem = positiveCharactersMap.ElementAtOrDefault(posStartIndex);
            code += posStartElem.Key;
            codeNum += posStartElem.Value;

            for (int i = 0; i < 4; i++)
            {
                var negIndex = random.Next(0, negativeCharactersMap.Count);
                var negElement = negativeCharactersMap.ElementAtOrDefault(negIndex);
                code += negElement.Key;
                codeNum += negElement.Value;
            }


            var posNextndex = random.Next(0, positiveCharactersMap.Count);
            var posNextElem = positiveCharactersMap.ElementAtOrDefault(posNextndex);
            code += posNextElem.Key;
            codeNum += posNextElem.Value;


            var negNextIndex = random.Next(0, negativeCharactersMap.Count);
            var negNextElem = negativeCharactersMap.ElementAtOrDefault(negNextIndex);
            code += negNextElem.Key;
            codeNum += negNextElem.Value;


            var posEndIndex = random.Next(0, positiveCharactersMap.Count);
            var posEndElem = positiveCharactersMap.ElementAtOrDefault(posEndIndex);
            code += posEndElem.Key;
            codeNum += posEndElem.Value;


        }
        else
        {

            var negStartIndex = random.Next(0, negativeCharactersMap.Count);
            var negStartElem = negativeCharactersMap.ElementAtOrDefault(negStartIndex);
            code += negStartElem.Key;
            codeNum += negStartElem.Value;


            for (int i = 0; i < 4; i++)
            {
                var posIndex = random.Next(0, positiveCharactersMap.Count);
                var posElement = positiveCharactersMap.ElementAtOrDefault(posIndex);
                code += posElement.Key;
                codeNum += posElement.Value;

            }


            var negNextndex = random.Next(0, negativeCharactersMap.Count);
            var negNextElem = negativeCharactersMap.ElementAtOrDefault(negNextndex);
            code += negNextElem.Key;
            codeNum += negNextElem.Value;


            var posNextIndex = random.Next(0, positiveCharactersMap.Count);
            var posNextElem = positiveCharactersMap.ElementAtOrDefault(posNextIndex);
            code += posNextElem.Key;
            codeNum += posNextElem.Value;


            var negEndIndex = random.Next(0, negativeCharactersMap.Count);
            var negEndElem = negativeCharactersMap.ElementAtOrDefault(negEndIndex);
            code += negEndElem.Key;
            codeNum += negEndElem.Value;

        }

        if (codeList.Contains(code) || codeNum != 0)
        {
            codeIndex--;
            continue;
        }
        codeList.Add(code);

        if (codeIndex % 100_000 == 0)
        {
            Console.WriteLine($"{codeIndex}");
        }
    }


    var json = JsonSerializer.Serialize(codeList);

    File.WriteAllText("codes.json", json);
}




public bool CheckCode(string code)
{

    char[] chars = code.ToCharArray();
    bool isValidCode = false;
    if (positiveCharactersMap.ContainsKey(chars[0]))
    {
        bool isNext4Valid = chars[1..4].All(x=> negativeCharactersMap.ContainsKey(x));
        bool isFifthValid = positiveCharactersMap.ContainsKey(chars[5]);
        bool isSixthValid = negativeCharactersMap.ContainsKey(chars[6]);
        bool isSeventhValid = positiveCharactersMap.ContainsKey(chars[7]);

        isValidCode = isNext4Valid && isFifthValid && isSixthValid && isSeventhValid;
    }
    else if (negativeCharactersMap.ContainsKey(chars[0]))
    {
        bool isNext4Valid = chars[1..4].All(x => positiveCharactersMap.ContainsKey(x));
        bool isFifthValid = negativeCharactersMap.ContainsKey(chars[5]);
        bool isSixthValid = positiveCharactersMap.ContainsKey(chars[6]);
        bool isSeventhValid = negativeCharactersMap.ContainsKey(chars[7]);
        isValidCode = isNext4Valid && isFifthValid && isSixthValid && isSeventhValid;
    }
    return  isValidCode && chars.Sum(x => allowedCharctersMap[x]) == 0;
}
}