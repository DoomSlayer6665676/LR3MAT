using LR1MAT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR3MAT
{
    struct Solution {  public int x; public bool flag; public Solution(int x, bool flag) { this.x = x; this.flag = flag; } }
    public struct Sup { public bool flag; public string[] column; public string[] row; public double F; public Sup() { this.flag = false; } }
    public class Branches_and_borders : Table
    {
        static double[,] reverse(double[,] Table, int str)
        {
            double[,] _Table = (double[,])Table.Clone();
            for (int i = 0; i < _Table.GetLength(1); i++) _Table[_Table.GetLength(0)-1, i] = -_Table[str, i];
            return _Table;
        }
        Solution is_int(double[] table) 
        { 
            for (int i = 0; i < table.Length; i++) if (Math.Round(table[i],2) % 1 != 0) return new Solution(i, false);
            return new Solution(0, true);
        }
        public Branches_and_borders(double[] c, double[,] A, double[] b, Sup branch) : base(c, A, b) 
        {
            if (branch.flag) 
            {
                for (int i = 0; i < branch.column.Length-1; i++)
                {
                    this.column[i] = branch.column[i];
                }
                for (int i = 0; i < branch.row.Length; i++)
                {
                    this.row[i] = branch.row[i];
                }
                this.simplex_table[this.simplex_table.GetLength(0)-1, 0] = branch.F;
            }
        }
        public void simplex_metod()
        {
            int Flag0 = base.simplex_metod();
            if (Flag0 != 0)
            {
                Console.WriteLine($"Задача не решается {Flag0}\nКонец ветви");
            }
            else
            {
                Solution flag1 = is_int(this.end_args);
                if (flag1.flag)
                {
                    Console.WriteLine("Найдено целое решение\nКонец ветви");
                }
                else
                {
                    Console.WriteLine($"Решение не является целым, {this.column[flag1.x]} дробное");
                    double[] new_b = new double[this.b.Length + 1];
                    for (int i = 0; i < this.b.Length; i++) new_b[i] = this.simplex_table[i, 0];
                    double[,] new_A = new double[this.simplex_table.GetLength(0), this.simplex_table.GetLength(1) - 1];
                    for (int i = 0; i < this.simplex_table.GetLength(0) - 1; ++i)
                    {
                        for (int j = 1; j < this.simplex_table.GetLength(1); ++j)
                        {
                            new_A[i, j - 1] = this.simplex_table[i, j];
                        }
                    }
                    for (int j = 1; j < this.simplex_table.GetLength(1); ++j)
                    {
                        new_A[this.simplex_table.GetLength(0)-1, j - 1] = this.simplex_table[flag1.x, j];
                    }
                    double[] new_c = new double[this.c.Length];
                    for (int i = 0; i < this.c.Length; ++i) new_c[i] = this.simplex_table[this.simplex_table.GetLength(0)-1, i + 1];
                    new_b[this.b.Length] = Math.Floor(this.simplex_table[flag1.x, 0]) - this.simplex_table[flag1.x, 0];
                    Console.WriteLine($"Ветвь {this.column[flag1.x]} <= {Math.Floor(this.simplex_table[flag1.x, 0])}");
                    Sup branch_args = new();
                    branch_args.flag = true; branch_args.column = this.column; branch_args.row = this.row; branch_args.F = this.simplex_table[this.simplex_table.GetLength(0)-1, 0];
                    Branches_and_borders branch1 = new Branches_and_borders(new_c, reverse(new_A, flag1.x), new_b, branch_args);
                    branch1.simplex_metod();
                    new_b[this.b.Length] = -Math.Floor(this.simplex_table[flag1.x, 0]) - 1 + this.simplex_table[flag1.x, 0];
                    Console.WriteLine($"Ветвь {this.column[flag1.x]} >= {Math.Floor(this.simplex_table[flag1.x, 0]) + 1}");
                    Branches_and_borders branch2 = new Branches_and_borders(new_c, new_A, new_b, branch_args);
                    branch2.simplex_metod();
                }
            }
        }
        public void Brute_force()
        {
            int max_arg = 0;
            foreach (double arg in this.b) if (arg > max_arg) max_arg = (int)Math.Floor(arg) + 1;
            for (int x1 = 0; x1 < max_arg; x1++)
            {
                for (int x2 = 0; x2 < max_arg; x2++)
                {
                    for (int x3 = 0; x3 < max_arg; x3++)
                    {
                        bool f = true;
                        for (int i = 0; i < this.A.GetLength(0); i++)
                        {
                            if (x1 * this.A[i, 0] + x2 * this.A[i, 1] + x3 * this.A[i, 2] > this.b[i]) { f = false; break; }
                        }
                        if (f)
                        {
                            int F = (int)(x1 * this.c[0] + x2 * this.c[1] + x3 * this.c[2]);
                            Console.WriteLine($"X1={x1}, X2={x2}, X3={x3}, F={F}");
                        }
                    }
                }
            }
        }
    }
}
