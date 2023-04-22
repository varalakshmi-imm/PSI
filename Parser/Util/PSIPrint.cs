// ⓅⓈⒾ  ●  Pascal Language System  ●  Academy'23
// PSIPrint.cs ~ Prints a PSI syntax tree in Pascal format
// ─────────────────────────────────────────────────────────────────────────────
namespace PSI;

public class PSIPrint : Visitor<StringBuilder> {
   public override StringBuilder Visit (NProgram p) {
      Write ($"program {p.Name};");
      Visit (p.Block);
      return Write (".");
   }

   public override StringBuilder Visit (NBlock b) 
      => Visit (b.Decls, b.Body);

   public override StringBuilder Visit (NDeclarations d) {
      if (d.Vars.Length > 0) Visit (d.Vars);
      for (int i = 0; i < d.Funcs.Length; i++) d.Funcs[i].Accept (this);
      return S;
   }

   public override StringBuilder Visit (NVarDecl d)
      => NWrite ($"{d.Name} : {d.Type}");

   public override StringBuilder Visit (NFnDecl f) {
      NWrite ($"{(f.Procedure ? "procedure" : "function")} {f.Name} (");
      foreach (var g in f.Vars.GroupBy (a => a.Type))
         Write ($"{g.Select (a => a.Name).ToCSV ()} : {g.Key};");
      Write (")");
      if (!f.Procedure) Write ($" : {f.Type}");
      Write (";");
      Visit (f.Block);
      return Write (";");
   }

   public override StringBuilder Visit (NCompoundStmt b) {
      NWrite ("begin"); N++; Visit (b.Stmts); N--; return NWrite ("end"); 
   }

   public override StringBuilder Visit (NAssignStmt a) {
      NWrite ($"{a.Name} := "); a.Expr.Accept (this); return Write (";");
   }

   public override StringBuilder Visit (NWriteStmt w) {
      NWrite (w.NewLine ? "WriteLn (" : "Write (");
      for (int i = 0; i < w.Exprs.Length; i++) {
         if (i > 0) Write (", ");
         w.Exprs[i].Accept (this);
      }
      return Write (");");
   }

   public override StringBuilder Visit (NReadStmt r) {
      NWrite ("read (");
      for (int i = 0; i < r.Tokens.Length; i++) {
         if (i > 0) Write (", ");
         Write (r.Tokens[i].Text);
      }
      return Write (");");
   }

   public override StringBuilder Visit (NCallStmt c) {
      NWrite ($"{c.Name} (");
      for (int i = 0; i < c.Exprs.Length; i++) {
         if (i > 0) Write (", ");
         c.Exprs[i].Accept (this);
      }
      return Write (");");
   }
   public override StringBuilder Visit (NIfStmt ifStmt) {
      NWrite ($"if ");
      ifStmt.Expr.Accept (this);
      Write (" then "); N++; ifStmt.Stmts[0].Accept (this); N--;
      for (int i = 1; i < ifStmt.Stmts.Length; i++) {
         NWrite ("else "); N++;
         ifStmt.Stmts[i].Accept (this);
         N--;
      }
      return S;
   }
   public override StringBuilder Visit (NForStmt f) {
      NWrite ($"for {f.Name} := ");
      f.Expr.Accept (this);
      Write ($" {f.ToDownto.Text} ");
      f.Expr2.Accept (this);
      Write (" do"); N++; f.Stmt.Accept (this);
      if (f.Stmt is NCompoundStmt) Write (";"); N--;
      return S;
   }

   public override StringBuilder Visit (NWhileStmt w) {
      NWrite ("While "); w.Expr.Accept (this);
      Write (" do "); w.Stmt.Accept (this);
      return Write (";");
   }

   public override StringBuilder Visit (NRepeatStmt r) {
      NWrite ("repeat"); N++;
      for (int i = 0; i < r.Stmts.Length; i++)
         r.Stmts[i].Accept (this);
      N--;
      NWrite ("until ");
      r.Expr.Accept (this);
      return Write (";");
   }

   public override StringBuilder Visit (NLiteral t)
      => Write (t.Value.ToString ());

   public override StringBuilder Visit (NIdentifier d)
      => Write (d.Name.Text);

   public override StringBuilder Visit (NUnary u) {
      Write (u.Op.Text); return u.Expr.Accept (this);
   }

   public override StringBuilder Visit (NBinary b) {
      Write ("("); b.Left.Accept (this); Write ($" {b.Op.Text} ");
      b.Right.Accept (this); return Write (")");
   }

   public override StringBuilder Visit (NFnCall f) {
      Write ($"{f.Name} (");
      for (int i = 0; i < f.Params.Length; i++) {
         if (i > 0) Write (", "); f.Params[i].Accept (this);
      }
      return Write (")");
   }

   StringBuilder Visit (params Node[] nodes) {
      nodes.ForEach (a => a.Accept (this));
      return S;
   }

   StringBuilder Visit (NVarDecl[] vars) {
      NWrite ("var"); N++;
      foreach (var g in vars.GroupBy (a => a.Type))
         NWrite ($"{g.Select (a => a.Name).ToCSV ()}: {g.Key};");
      N--;
      return S;
   }

   // Writes in a new line
   StringBuilder NWrite (string txt) 
      => Write ($"\n{new string (' ', N * 3)}{txt}");
   int N;   // Indent level

   // Continue writing on the same line
   StringBuilder Write (string txt) {
      Console.Write (txt);
      S.Append (txt);
      return S;
   }

   readonly StringBuilder S = new ();
}