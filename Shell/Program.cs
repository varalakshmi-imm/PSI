using PSI;

static class Start {
   static void Main () {
      foreach (var file in Directory.GetFiles ("P:/Shell/Demo", "*.pas")) {
         NProgram? node;
         try {
            Console.WriteLine ();
            var text = File.ReadAllText (file);
            node = new Parser (new Tokenizer (text)).Parse ();
            node.Accept (new TypeAnalyze ());
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