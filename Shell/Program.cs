using PSI;

static class Start {
   static void Main () {
      foreach (var file in Directory.GetFiles (@"P:/Shell/Demo", "*.pas")) {
         NProgram? node;
         try {
            Console.WriteLine ();
            var text = File.ReadAllText (file);
            var t = new Tokenizer (text) { FileName = Path.GetFileName (file) };
            node = new Parser (t).Parse ();
            node.Accept (new PSIPrint ());
         } catch (ParseException pe) {
            Console.WriteLine ();
            pe.Print ();
         } catch (Exception e) {
            Console.WriteLine ();
            Console.WriteLine (e);
         }
      }
   }
}