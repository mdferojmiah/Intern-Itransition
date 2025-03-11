var num = [1, 2, 3, 4]

// var a = num.map(fun);
// function fun(n){
//     return n+1;
// }

// array.map(function(currentValue, index, arr))
// var a = num.map((n, i, num) =>{
//     return n+i;
// });
// console.log(a);

let map = function(arr, fn){
    let newArr = []
    for(let i = 0; i < arr.length; i++){
        newArr[i] = fn(arr[i], i);
    }
    return newArr;
}

let arr = [1,2,3]
let fn = function plusOne(n, i){
    return n+1;
}
console.log(map(arr, fn));