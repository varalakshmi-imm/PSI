program Comp14;
var
   name: string;

procedure Greeter (msg: string);
var
   name: string;
begin
   write ("Enter your name: ");
   readln (name);
   writeln ("Hello, ", name, ". ", msg);
end;


begin
   Greeter ("Have a good day!");
end.