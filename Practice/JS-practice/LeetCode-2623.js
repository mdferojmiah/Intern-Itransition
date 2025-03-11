function square(n){
    return n*n;
}

function memoize(func){
    let cache = {};
    return function(...args){
        let n = args[0];
        if(n in cache){
            return cache[n];
        }else{
            let result = func(n)
            cache[n] = result;
            return result;
        }
    }
}

console.time();
let result = memoize(square);
console.log(result(5));
console.timeEnd();

console.time();
console.log(result(5));
console.timeEnd();