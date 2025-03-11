function add(args){
    return args[0] + args[1];
}

function mulTwo(a){
    return a *2;
}

function square(a){
    return a*a;
}

// function addSquare(a, b){
//     return square(add(a,b));
// }
// console.log(addSquare(2,3));
// traditional form
    // function compose(f1, f2){
    //     return function(a, b){
    //         return f2(f1(a, b));
    //     }
    // }
// modern javascript
    // const composeTwo = (f1, f2, f3) => (a, b) => f3(f2(f1(a, b)));
    // let result = composeTwo(add, mulTwo, square);
    // console.log(result(3,3));

//Composition of unlimited function
    // function composeAll(...funs){
    //     return function(...values){
    //         return funs.reduce((val, fn) => fn(val), values);
    //     }
    // }

    // const result = composeAll(add, mulTwo, square);
    // console.log(result(2,3));

let compose = function(functions){
    return function(x){
        for(let i = functions.length - 1; i >= 0; i--){
            x = functions[i](x);
        }
        return x;
    }
}