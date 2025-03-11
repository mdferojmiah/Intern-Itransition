var createCounter = function (n){
    let count = n;
    //a function can use any variable from its closure
    // though count is not declared in the belowed arrow function
    // but it can use it because of closure
    return function(){
        return ++count;
    }
}

console.log(createCounter(3)());