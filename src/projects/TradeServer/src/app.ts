import { RunService } from "./main";

// RunService();
console.log(this === global);
// console.log("hello world");

let myfun = function(name: any){
    console.log(this);
}

myfun("some name");