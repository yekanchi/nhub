import { RunService } from "./main";

// RunService();

let myfun = function(name: any){
    console.log(this);
}

myfun("some name");