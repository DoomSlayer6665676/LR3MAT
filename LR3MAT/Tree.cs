using System;
using System.Collections.Generic;

namespace LR3MAT
{
    // Класс для узла дерева решений
    public class TreeNode
    {
        public string Value { get; set; }
        public List<TreeNode> Children { get; set; }
        public bool IsLeaf => Children == null || Children.Count == 0;

        public TreeNode(string value)
        {
            Value = value;
            Children = new List<TreeNode>();
        }

        public void AddChild(TreeNode child)
        {
            Children.Add(child);
        }
    }

    // Расширенный класс для отображения дерева в консоли
    public class TreePrinter
    {
        public static void PrintTree(TreeNode node, string indent = "", bool isLast = true)
        {
            // Рисуем маркер для текущего узла
            var marker = isLast ? "└── " : "├── ";
            Console.Write(indent);
            Console.Write(marker);

            // Разные цвета для листовых и промежуточных узлов
            if (node.IsLeaf)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(node.Value);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(node.Value);
                Console.ResetColor();
            }

            // Обновляем отступ для дочерних узлов
            indent += isLast ? "    " : "│   ";

            // Рекурсивно выводим дочерние узлы
            for (int i = 0; i < node.Children.Count; i++)
            {
                var isLastChild = i == node.Children.Count - 1;
                PrintTree(node.Children[i], indent, isLastChild);
            }
        }

        // Альтернативный метод отображения с дополнительной информацией
        public static void PrintTreeDetailed(TreeNode node, string path = "Root")
        {
            Console.WriteLine($"{path}: {node.Value} {(node.IsLeaf ? "[ЛИСТ]" : "[УЗЕЛ]")}");

            foreach (var child in node.Children)
            {
                PrintTreeDetailed(child, $"{path} -> {node.Value}");
            }
        }
        // Статистика дерева
        public static void PrintTreeStats(TreeNode node)
        {
            int totalNodes = CountNodes(node);
            int leafNodes = CountLeafNodes(node);
            int maxDepth = GetMaxDepth(node);

            Console.WriteLine($"Всего узлов: {totalNodes}");
            Console.WriteLine($"Листовых узлов: {leafNodes}");
            Console.WriteLine($"Максимальная глубина: {maxDepth}");
            Console.WriteLine($"Промежуточных узлов: {totalNodes - leafNodes}");
        }

        static int CountNodes(TreeNode node)
        {
            if (node == null) return 0;
            int count = 1;
            foreach (var child in node.Children)
            {
                count += CountNodes(child);
            }
            return count;
        }

        static int CountLeafNodes(TreeNode node)
        {
            if (node == null) return 0;
            if (node.IsLeaf) return 1;
            int count = 0;
            foreach (var child in node.Children)
            {
                count += CountLeafNodes(child);
            }
            return count;
        }

        static int GetMaxDepth(TreeNode node)
        {
            if (node == null) return 0;
            if (node.IsLeaf) return 1;

            int maxDepth = 0;
            foreach (var child in node.Children)
            {
                maxDepth = Math.Max(maxDepth, GetMaxDepth(child));
            }
            return maxDepth + 1;
        }
    }
}