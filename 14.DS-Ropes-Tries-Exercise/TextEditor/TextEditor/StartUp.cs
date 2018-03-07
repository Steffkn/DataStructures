using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        ITextEditor textEditor = new TextEditor();

        Regex regex = new Regex("(?<=\")(?<text>.*)(?=\")");

        string input = string.Empty;

        while ((input = Console.ReadLine()) != "end")
        {
            Match matchText = regex.Match(input);

            string[] tokens = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            switch (tokens[0])
            {
                case "login":
                    string userName = tokens[1];
                    textEditor.Login(userName);
                    break;
                case "logout":
                    userName = tokens[1];
                    textEditor.Logout(userName);
                    break;
                case "users":
                    List<string> users;
                    if (tokens.Length > 1)
                    {
                        users = textEditor.Users(tokens[1]).ToList();
                    }
                    else users = textEditor.Users().ToList();
                    PrintUsers(users);
                    break;
                default:
                    userName = tokens[0];

                    string text = matchText.Groups["text"].Value;

                    switch (tokens[1])
                    {
                        case "insert":
                            if (!matchText.Success) continue;
                            textEditor.Insert(userName, int.Parse(tokens[2]), text);
                            break;
                        case "prepend":
                            if (!matchText.Success) continue;
                            textEditor.Prepend(userName, text);
                            break;
                        case "substring":
                            textEditor.Substring(userName, int.Parse(tokens[2]), int.Parse(tokens[3]));
                            break;
                        case "delete":
                            textEditor.Delete(userName, int.Parse(tokens[2]), int.Parse(tokens[3]));
                            break;
                        case "clear":
                            textEditor.Clear(userName);
                            break;
                        case "length":
                            Console.WriteLine(textEditor.Length(userName));
                            break;
                        case "print":
                            var printResult = textEditor.Print(userName);
                            if (printResult != null)
                                Console.WriteLine(printResult);
                            break;
                        case "undo":
                            textEditor.Undo(userName);
                            break;
                    }
                    break;
            }
        }
    }


    private static void PrintUsers(List<string> users)
    {
        foreach (var user in users)
        {
            Console.WriteLine(user);
        }
    }
}