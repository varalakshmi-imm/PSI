namespace PSI;
using static System.Console;
using static Token.E;

// Represents a PSI language Token
public class Token {
   public Token (Tokenizer source, E kind, string text, int line, int column) 
      => (Source, Kind, Text, Line, Column) = (source, kind, text, line, column);
   public Tokenizer Source { get; }
   public E Kind { get; }
   public string Text { get; }
   public int Line { get; }
   public int Column { get; }

   // The various types of token
   public enum E {
      // Keywords
      PROGRAM, VAR, IF, THEN, WHILE, ELSE, FOR, TO, DOWNTO,
      DO, BEGIN, END, PRINT, TYPE, NOT, OR, AND, MOD, _ENDKEYWORDS,
      // Operators
      ADD, SUB, MUL, DIV, NEQ, LEQ, GEQ, EQ, LT, GT, ASSIGN, 
      _ENDOPERATORS,
      // Punctuation
      SEMI, PERIOD, COMMA, OPEN, CLOSE, COLON, 
      _ENDPUNCTUATION,
      // Others
      IDENT, INTEGER, REAL, BOOLEAN, STRING, CHAR, EOF, ERROR
   }

   // Print a Token
   public override string ToString () => Kind switch {
      EOF or ERROR => Kind.ToString (),
      < _ENDKEYWORDS => $"\u00ab{Kind.ToString ().ToLower ()}\u00bb",
      STRING => $"\"{Text}\"",
      CHAR => $"'{Text}'",
      _ => Text,
   };

   // Utility function used to echo an error to the console
   public void PrintError () {
      if (Kind != ERROR) throw new Exception ("PrintError called on a non-error token");
      int start = Line - 3, end = Line + 2;
      OutputEncoding = Encoding.Unicode;
      var hdr = $"File: {Source.FileName}";
      WriteLine (hdr);
      WriteLine (new string ('─', hdr.Length));
      for (int i = start; i < end; i++) {
         if (i < 0 || i >= Source.Lines.Length) continue;
         var line = $"{i + 1,4}│" + Source.Lines[i];
         WriteLine (line);
         if (i == Line  - 1) { // This is the line that has error
            int col = Column + 5; // As we have added line number as four digits and the vertical line
            ForegroundColor = ConsoleColor.Yellow;
            var err = new string (' ', line.Length).ToCharArray ();
            err[col - 1] = '^';
            WriteLine (new string (err));
            CursorLeft = Math.Clamp (col - Text.Length / 2, 0, WindowWidth - Text.Length);
            WriteLine (Text);
            ResetColor ();
         }
      }
   }

   // Helper used by the parser (maps operator sequences to E values)
   public static List<(E Kind, string Text)> Match = new () {
      (NEQ, "<>"), (LEQ, "<="), (GEQ, ">="), (ASSIGN, ":="), (ADD, "+"),
      (SUB, "-"), (MUL, "*"), (DIV, "/"), (EQ, "="), (LT, "<"),
      (LEQ, "<="), (GT, ">"), (SEMI, ";"), (PERIOD, "."), (COMMA, ","),
      (OPEN, "("), (CLOSE, ")"), (COLON, ":")
   };
}
