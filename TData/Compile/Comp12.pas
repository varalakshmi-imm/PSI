program Comp12;
var 
  g: integer;

function Min2 (a, b: integer): integer; 
begin 
  if a < b then
    Min2 := a;
  else
    Min2 := b;
end;
  
begin
  g := Min2 (13, 5);
  WriteLn ("Minimum (5, 13) = ", g);
  if 3 < 4 then begin
     WriteLn ("Three is less than four.");
	  WriteLn ("But you know that already.");
  end;
end.