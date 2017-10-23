using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Tranzlatory
{
    public class Item
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return this.Category + " " + this.Value;
        }

        private static bool regex_aZ(String value)
        {
            return Regex.IsMatch(value, @"^[a-zA-Z]+$");
        }

        private static bool regex1to9(String value)
        {
            return Regex.IsMatch(value, @"^[0-9]+$");
        }

        public static void getDataFromTxt(string path, List<Item> itemList)
        {
            //path = "C:\\Users\\Fuger\\Desktop\\test.txt";
            string data = File.ReadAllText(path);
            int x = data.Length;
            int i = 0;
            int id = 1;

            while (i <= x)
            {
                string value = "";
                value += data[i];
                
                if (regex1to9(value))
                {
                    bool endStatus = false;
                    bool dotStatus = false;

                    while (!endStatus)
                    {
                            i++;
                            if (i == x)
                            {
                                if (dotStatus == false)
                                {
                                    itemList.Add(new Item { Id = id, Category = "Liczba", Value = value });
                                    id++;
                                break;
                                }
                                else
                                {
                                    itemList.Add(new Item { Id = id, Category = "Liczba zmiennoprzecinkowa", Value = value });
                                    id++;
                                break;
                                }
                            }

                        if (data[i] == '1' || data[i] == '2' || data[i] == '3' || data[i] == '4' ||
                            data[i] == '5' || data[i] == '6' || data[i] == '7' || data[i] == '8' || data[i] == '9')
                        {
                            value += data[i];
                        }
                        else if (data[i] == '.')
                        {
                            if (dotStatus == false)
                            {
                                dotStatus = true;

                                if (data[i + 1] == '0' || data[i + 1] == '1' || data[i + 1] == '2'
                                 || data[i + 1] == '3' || data[i + 1] == '4' || data[i + 1] == '5'
                                 || data[i + 1] == '6' || data[i + 1] == '7' || data[i + 1] == '8' || data[i + 1] == '9')
                                {
                                    value += data[i];
                                }
                            }
                            else if (dotStatus == true)
                            {
                                itemList.Add(new Item { Id = id, Category = "Liczba zmiennoprzecinkowa", Value = value });
                                id++;
                                endStatus=true;
                            }
                        }
                        else
                        {
                            if (dotStatus == false)
                            {
                                itemList.Add(new Item { Id = id, Category = "Liczba", Value = value });
                                id++;
                            }
                            else
                            {
                                itemList.Add(new Item { Id = id, Category = "Liczba zmiennoprzecinkowa", Value = value });
                                id++;
                            }
                            endStatus = true;
                        }
                    }
                }

                else if (regex_aZ(value))
                {
                    bool endStatus = false;

                    while (!endStatus)
                    {
                    if (i + 1 == x)
                    {
                        itemList.Add(new Item { Id = id, Category = "Identyfikator", Value = value });
                        id++;
                        i++;
                        break;
                    }
     
                        char temp = data[i + 1];

                        if (data[i + 1] == '1' || data[i + 1] == '2' || data[i + 1] == '3' || data[i + 1] == '4' || data[i + 1] == '5'
                     || data[i + 1] == '6' || data[i + 1] == '7' || data[i + 1] == '8' || data[i + 1] == '9')

                        {
                            i++;
                            value += data[i];
                        }

                    else if (regex_aZ(temp.ToString()))
                        {
                            i++;
                            value += data[i];
                        }

                    else
                        {
                            itemList.Add(new Item { Id = id, Category = "Identyfikator", Value = value });
                            id++;
                            i++;
                            endStatus = true;
                        }
                    }
                }

                else if (value.Contains("+") || value.Contains("-") || value.Contains("/") || value.Contains("*") || value.Contains("="))
                {
                    itemList.Add(new Item { Id = id, Category = "Operator", Value = value });
                    id++;
                   // MessageBox.Show("Operator " + value);
                    i++;
                }

                else if (value.Contains("(") || value.Contains(")"))
                {
                    itemList.Add(new Item { Id = id, Category = "Nawias", Value = value });
                    id++;
                    i++;
                }

                else if (value.Contains(" ") || value.Contains("\n") || value.Contains("\t") || value.Contains("\r"))
                {
                    i++;
                }

                else
                {
                    itemList.Add(new Item { Id = id, Category = "Błąd!", Value = "" });
                    id++;
                    break;
                }
                if (i == x)
                {
                    break;
                }
            }
        }
    }
}
            
