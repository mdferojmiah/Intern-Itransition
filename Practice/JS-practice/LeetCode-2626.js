// blueprint of redure mathod is:
// array.reduce(function(total, currentValue, currentIndex, arr))
// here total and currentValue is compulsory others are optional arguments
        // let arr = [1, 2, 3, 4, 5];
        // let sum =  arr.reduce(addNum);
        // function addNum(a, b){
        //     return a + b;
        // }
        // console.log(sum);

let reduce = function(nums, fn, init){
    let total = init;
    for(let i = 0; i < nums.length; i++){
        total = fn(total, nums[i]);
    }
    return total;
}

let nums = [1, 2, 3, 4]
function sum(accum, curr){
    return accum + curr;
}
let init = 0;

console.log(reduce(nums, sum, 0));