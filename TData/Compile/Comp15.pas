program nestedPrime;
var
   i, j:integer;

begin
   for i := 2 to 50 do
   begin
      for j := 2 to i do
         if (i mod j) = 0  then
            break;
      
      if(j = i) then
         writeln(i , " is prime" );
         
   if (i > 10) then break;
   end;
end.