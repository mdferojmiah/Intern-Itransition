let arr = [19, 12, 18, 20, 15]

// let newArr = arr.filter((age) =>{
//     return age >= 18;
// })
// console.log(newArr);

// let newArr = arr.filter(fn);
// function fn(age) {
//     return age >= 18;
// }
// console.log(newArr);

let filter = function(arr, fn){
    let newArr = []

    for(let i = 0; i < arr.length; i++){
        if(fn(arr[i], i)){
            newArr.push(arr[i]);
        }
    }
    return newArr;
}

function fn(n, i){
    return n >= 15;
}

console.log(filter(arr, fn));