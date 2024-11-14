﻿using System.Text.RegularExpressions;

namespace Day08Part1
{
    internal partial class Node
    {
        private static List<Node> _allNodes = new List<Node>();
        public static void ApplyAllSubNodes()
        {
            foreach(Node node in _allNodes)
            {
                node.ApplySubNodes();
            }
        }

        public static Node GetNodeByName(string name) => _allNodes.Single(n => n.Id == name);

        private string _leftNodeName;
        private string _rightNodeName;

        public string Id { get; set; }
        public Node? Left { get; private set; }
        public Node? Right { get; private set; }

        public static void CreateNodeFromString(string str)
        {
            _allNodes.Add(new Node(str));
        }

        [GeneratedRegex("(?<id>[A-Z]{3}) = \\((?<left>[A-Z]{3}), (?<right>[A-Z]{3})\\)")]
        private static partial Regex DefinitionRegex();

        public Node(string definition)
        {
            Match match = DefinitionRegex().Match(definition);
            if (!match.Success)
            {
                throw new ArgumentException("Incorrect format", nameof(definition));
            }
            Id = match.Groups["id"].Value;
            _leftNodeName = match.Groups["left"].Value;
            _rightNodeName = match.Groups["right"].Value;
        }

        public void ApplySubNodes()
        {
            Left = _allNodes.Single(n => n.Id == _leftNodeName);
            Right = _allNodes.Single(n => n.Id == _rightNodeName);
        }
    }
}
