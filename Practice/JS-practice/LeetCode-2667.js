// //normal function
// function f(a, b){
//     return sum = a + b;
// }

// console.log(f(2,3));

// //anonymous function
// //it has no name. rather we use a variable to create the function.
// // we also can call the function using the varibale name.
// // parameter can be passed beside the variable name.
// let add = function (a, b){
//     return a + b;
// }

// console.log(add(4, 5));

// //immediately invoked function
// //function is called when it is being created
// let sub = function(a, b){
//     return a - b;
// }(6, 5) // (a, b) parameter is used beside the function declation
// console.log(sub);

// // arrow funnction
// // differnt apporch to declaring a function
// let mul = (a, b) =>{
//     return a*b;
// }
// console.log(mul(5, 4));

// // function within function
// function fun(){
//     function f(a, b){
//         return a / b;
//     }
//     return f;
// }

// var div = fun(); // fun() will return the function and the function will be assigned in the div
// console.log(div(6, 3)); // div here will work as a anonymous 

var creatHelloWorld = function(){
    return function(){
        return "Hello World"
    }
}

console.log(creatHelloWorld()());