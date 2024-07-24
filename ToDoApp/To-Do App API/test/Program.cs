// See https://aka.ms/new-console-template for more information
using System.Collections.Immutable;
using System.Runtime.InteropServices;

Console.WriteLine("Hello, World!");
int[] arr = { 1, 3, -6, 8, 8, 2, 9 };
int temp = 0;
for (int i = 0; i < arr.Length; i++)
{
    for (int j = i + 1; j < arr.Length; j++)
    {
        if (arr[i] > arr[j])
        {
            temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
    }
}
for(int i=arr.Length; i >0; i++)
{
    if()
}
    Console.WriteLine( arr[arr.Length - 2]);
Console.ReadLine();
