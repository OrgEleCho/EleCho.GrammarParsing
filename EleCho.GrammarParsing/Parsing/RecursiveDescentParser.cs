using EleCho.GrammarParsing.Tokenizing;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleCho.GrammarParsing.Parsing
{

    public class RecursiveDescentParser : Parser
    {
        public bool EnableLookAhead { get; set; } = true;

        public RecursiveDescentParser(Grammar grammar) : base(grammar)
        {
        }

        private bool TerminalMatch(TerminalTerm term, Token token)
        {
            return
                term.RequiredTokenKind == token.Kind &&
                (term.RequiredTokenText is null || term.RequiredTokenText == token.Text);
        }

        private bool TryParseTerm(
            ITokenStream tokenStream, Term term, int offset,
            [NotNullWhen(true)] out ParseTreeNode? node, out int tokensConsumed)
        {
            if (term is NonTerminalTerm nonTerminalTerm)
            {
                return TryParseNonTerminal(tokenStream, nonTerminalTerm, offset, out node, out tokensConsumed);
            }
            else if (term is TerminalTerm terminalTerm)
            {
                return TryParseTerminal(tokenStream, terminalTerm, offset, out node, out tokensConsumed);
            }
            else
            {
                throw new ArgumentException("Invalid term", nameof(term));
            }
        }

        private bool TryParseNonTerminal(
            ITokenStream tokenStream, NonTerminalTerm nonTerminalTerm, int offset,
            [NotNullWhen(true)] out ParseTreeNode? node, out int tokensConsumed)
        {
            if (nonTerminalTerm.Rule is null)
            {
                throw new ParseException($"No rule specified for {nonTerminalTerm}");
            }

            var peekToken = tokenStream.Peek(offset);
            var childNodes = new List<ParseTreeNode>();
            foreach (var branch in nonTerminalTerm.Rule)
            {
                var success = true;
                var currentBranchOffset = 0;

                childNodes.Clear();
                foreach (var term in branch)
                {
                    if (!TryParseTerm(tokenStream, term, offset + currentBranchOffset, out var childNode, out var termTokensConsumed))
                    {
                        success = false;
                        break;
                    }

                    currentBranchOffset += termTokensConsumed;
                    childNodes.Add(childNode);
                }

                if (success)
                {
                    node = new ParseTreeNode(nonTerminalTerm, childNodes, null);
                    tokensConsumed = currentBranchOffset;
                    return true;
                }
            }

            node = null;
            tokensConsumed = 0;
            return false;
        }

        private bool TryParseTerminal(
            ITokenStream tokenStream, TerminalTerm terminalTerm, int offset,
            [NotNullWhen(true)] out ParseTreeNode? node, out int tokensConsumed)
        {
            var token = tokenStream.Peek(offset);
            if (!TerminalMatch(terminalTerm, token))
            {
                node = null;
                tokensConsumed = 0;
                return false;
            }

            node = new ParseTreeNode(terminalTerm, [], token);
            tokensConsumed = 1;
            return true;
        }

        private ParseTreeNode ParseNonTerminal(ITokenStream tokenStream, NonTerminalTerm nonTerminalTerm)
        {
            if (!TryParseNonTerminal(tokenStream, nonTerminalTerm, 0, out var node, out var tokensConsumed))
            {
                throw new ParseException($"No match rule for {nonTerminalTerm}");
            }

            for (int i = 0; i < tokensConsumed; i++)
            {
                tokenStream.Read();
            }

            return node;
        }

        private ParseTreeNode ParseTerminal(ITokenStream tokenStream, TerminalTerm terminalTerm)
        {
            if (!TryParseTerminal(tokenStream, terminalTerm, 0, out var node, out var tokensConsumed))
            {
                throw new ParseException($"Unexpected token {tokenStream.Peek(0)}");
            }

            for (int i = 0; i < tokensConsumed; i++)
            {
                tokenStream.Read();
            }

            return node;
        }

        public override ParseTree Parse(ITokenStream tokenStream)
        {
            if (Grammar.Root is not { } rootTerm)
            {
                throw new InvalidOperationException("Invalid grammar");
            }

            var rootNode = ParseNonTerminal(tokenStream, rootTerm);
            return new ParseTree(rootNode);
        }
    }
}
