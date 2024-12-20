﻿using MathNet.Numerics.LinearAlgebra;

namespace AOC2024.Puzzles;

public abstract class PuzzleBase
{
    public abstract void Part1();
    public abstract void Part2();
    
    protected void ReadFileLineByLine(string file, Action<string> action)
    {

        if (!File.Exists(file))
        {
            throw new FileNotFoundException("File not found");
        }

        StreamReader rdr = new StreamReader(file);

        string line;

        while ((line = rdr.ReadLine()) != null)
        {
            action.Invoke(line);    
        }
            
        rdr.Close();
    }

    protected string ReadFullFile(string file)
    {
        if (!File.Exists(file))
        {
            throw new FileNotFoundException("File not found");
        }

        StreamReader rdr = new StreamReader(file);

        string content = rdr.ReadToEnd().Trim();
        rdr.Close();
        return content;

    }
    
    protected Matrix<float> ReadMatrixChar(string file, Func<char, float> handleChar)
    {
       
        string input = ReadFullFile(file);
        string[] lines = input.Split("\r\n");
        int rows = lines.Length;
        int cols = lines[0].Trim().Length;

        Matrix<float> m = Matrix<float>.Build.Dense(rows, cols, ' ');


        for (int j = 0; j < rows; j++)
        {
            for (int k = 0; k < cols; k++)
            {
                m[j, k] = handleChar.Invoke(lines[j][k]);
            }
        }

        return m;
    }
    
    protected void PrintMatrix<T>(Matrix<T> matrix, string separator = " ")
        where T: struct, IFormattable, IEquatable<T>
    {

            
        //Console.WriteLine(matrix);

        for (int r = 0; r < matrix.RowCount; r++)
        {
            for (int c = 0; c < matrix.ColumnCount; c++)
            {
                Console.Write(matrix[r, c].ToString("G0", System.Globalization.CultureInfo.InvariantCulture) + separator);
            }
            Console.WriteLine("");
        }
            
        //for (int x = 0; x < matrix.ColumnCount; x++)
        //{

        //}
    }
    
    protected void PrintMatrix<T>(Matrix<T> matrix, Func<T, string> handleValue)
        where T: struct, IFormattable, IEquatable<T>
    {

            
        //Console.WriteLine(matrix);

        for (int r = 0; r < matrix.RowCount; r++)
        {
            for (int c = 0; c < matrix.ColumnCount; c++)
            {
                Console.Write(handleValue(matrix[r, c]));
            }
            Console.WriteLine("");
        }
            
        //for (int x = 0; x < matrix.ColumnCount; x++)
        //{

        //}
    }
}