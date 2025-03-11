//array literals
let arr = []
arr[0] = 'Feroj'
arr[1] = 'Miah'

//constuctor pattern
let a1 = new Array(5); // constructor pattern helps to create an array using mentioning length
let a2 = new Array(1, 2, 3, 4, 5)

//factory pattern
// factory pattern = constructor pattern
let b1 = Array(3)
let b2 = Array(6, 7, 8)

console.log(arr, arr.length)
console.log(a1, a1.length)
console.log(a2, a2.length)
console.log(b1, b1.length)
console.log(b2, b2.length)

for(let v of arr){
    console.log(v)
}

for(let x of a2){
    console.log(x)
}

for(let x of b2){
    console.log(x)
}

console.log(a1.__proto__.constructor)
console.log(b1.__proto__.constructor)
console.log(arr.__proto__.constructor)