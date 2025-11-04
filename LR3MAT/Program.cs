using LR3MAT;
double[] c = { 5, 6, 4 };
double[,] A = { { 1, 1, 1 },
{1, 3, 0 },
{0, 0.5, 4 } };
double[] b = { 7, 8, 6 };
Branches_and_borders Table = new Branches_and_borders(c, A, b, new Sup());
Table.simplex_metod();
Table.print_Tree();
Table.Brute_force();
