// object is a collection of key value pairs

//object literals
// var obj = {
//     name: "Feroj",
//     age: 24,
//     greet(name){
//         return "Good morning " + name;
//     }
// }
// console.log(obj)
// console.log(obj.name, obj.age)
// console.log(obj.greet(obj.name));

//objects within function
// function calc(val){
//     return {
//         add(val1){
//             return val + val1;
//         },
//         sub(val1){
//             return val - val1;
//         }
//     }
// }
// console.log(calc(5).add(3))
// console.log(calc(5).sub(5))

// function calc(val){
//     function add(val1){
//         return val + val1;
//     }
//     function sub (val1){
//         return val - val1;
//     }

//     //to return multiple function form a function we must use object
//     return {
//         add, sub
//     }
// }

// console.log(calc(5).add(5))
// console.log(calc(10).sub(5))

var expect = function(val){
    return {
        toBe: (val1) => {
            if(val !== val1){
                throw new Error("Not Equal");
            } else {
                return true;
            }
        },
        notToBe: (val1) =>{
            if(val === val1){
                throw new Error("Equal");
            } else {
                return true;
            }
        }
    }
}

console.log(expect(5).toBe(5));
// console.log(expect(5).toBe(null)); // throws an error: not equal
// console.log(expect(5).notToBe(5)); // throws an error: equal
console.log(expect(5).notToBe(null));
