using System;
using System.Collections.Generic;
using System.Linq;

using Encryption;

namespace Ciphers
{
    //Wrapper for 3-dimensional char array, to access elements by row using an index
    class GridByRows : IDisposable
    {
        public char[,,] Grid { get; set; }

        public char this[int i]
        {
            get
            {
                return Grid [
                                i / (Grid.GetLength(1) * Grid.GetLength(2)),
                                i % Grid.GetLength(1),
                                (i / Grid.GetLength(1)) % Grid.GetLength(2)
                            ];
            }
            set
            {
                Grid[
                        i / (Grid.GetLength(1) * Grid.GetLength(2)),
                        i % Grid.GetLength(1),
                        (i / Grid.GetLength(1)) % Grid.GetLength(2)
                    ]
                    = value;
            }
        }

        public int Size
        {
            get
            {
                return Grid.Length;
            }
        }

        public GridByRows(char[,,] newGrid)
        {
            Grid = newGrid;
        }

        public void Dispose() { }
    }

    //Wrapper for 3-dimensional char array, to access elements by column using an index
    class GridByColumns : IDisposable
    {
        public char[,,] Grid { get; set; }

        public char this[int i]
        {
            get
            {
                return Grid[
                                i / (Grid.GetLength(1) * Grid.GetLength(2)),
                                (i / Grid.GetLength(2)) % Grid.GetLength(1),
                                i % Grid.GetLength(2)
                            ];
            }
            set
            {
                Grid[
                        i / (Grid.GetLength(1) * Grid.GetLength(2)),
                        (i / Grid.GetLength(2)) % Grid.GetLength(1),
                        i % Grid.GetLength(2)
                    ]
                    = value;
            }
        }

        public int Size
        {
            get
            {
                return Grid.Length;
            }
        }

        public GridByColumns(char[,,] newGrid)
        {
            Grid = newGrid;
        }

        public void Dispose() { }
    }

    //Width-height key formatted for the cipher method
    class GridShape
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Area
        {
            get
            {
                return Width * Height;
            }
        }

        public GridShape(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }

    //Transposition Cipher
    //Writes plain text into grid by row, reads it out by column
    [CipherClass("Transposition Cipher", "1.0", "1.0")]
    public class TCipher : ICipher
    {
        public List<SubKey> KeyStructure
        {
            get
            {
                List<SubKey> returnValue = new List<SubKey>();
                returnValue.Add(new SubKey(typeof(int), "width"));
                returnValue.Add(new SubKey(typeof(int), "height"));
                return returnValue;
            }
        }

        public char[] Encrypt(char[] plainText, List<dynamic> key)
        {
            char[,,] grid;
            char[] cipherText;
            int i;
            GridShape gridShape;

            gridShape = new GridShape(key[0], key[1]);

            //Array of 2D grids, size as given by gridShape
            grid = new char[(plainText.Length - 1) / gridShape.Area + 1, gridShape.Width, gridShape.Height];

            //Filling up the grid by rows, first with the plain text then underscores as fillers
            using (var myGrid = new GridByRows(grid))
            {
                for (i = 0; i < plainText.Length; i++)
                {
                    myGrid[i] = plainText[i];
                }
                for (; i < myGrid.Size; i++)
                {
                    myGrid[i] = '_';
                }
            }

            //Reading from grid column by column to fill cipherText
            using (var myGrid = new GridByColumns(grid))
            {
                cipherText = new char[myGrid.Size];
                for (i = 0; i < myGrid.Size; i++)
                {
                    cipherText[i] = myGrid[i];
                }
            }

            return cipherText;
        }

        public char[] Decrypt(char[] cipherText, List<dynamic> key)
        {
            char[,,] grid;
            char[] plainText;
            char character;
            int i, j;
            GridShape gridShape;

            gridShape = new GridShape(key[0], key[1]);

            grid = new char[(cipherText.Length - 1) / gridShape.Area + 1, gridShape.Width, gridShape.Height];

            //Filling up the grid by columns from the cipher text
            using (var myGrid = new GridByColumns(grid))
            {
                for (i = 0; i < cipherText.Length; i++)
                {
                    myGrid[i] = cipherText[i];
                }
            }

            //Reading from grid row by row to fill plainText, ignoring underscore fillers
            using (var myGrid = new GridByRows(grid))
            {
                plainText = new char[myGrid.Size];
                for (i = 0, j = 0; i < myGrid.Size; i++)
                {
                    character = myGrid[i];
                    if ('_' != character)
                    {
                        plainText[j] = character;
                        j++;
                    }
                }
            }

            return plainText;
        }
    }
}
