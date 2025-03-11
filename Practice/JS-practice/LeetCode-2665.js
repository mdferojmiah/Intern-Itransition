// let createCounter = function(init){
//     let current = init;
//     return {
//         increment(){
//             return ++current;
//         },
//         decrement(){
//             return --current;
//         },
//         reset(){
//             return current = init;
//         }
//     }
// }

let createCounter = function(init){
    let current = init;
    
        function increment(){
            return ++current;
        }
        function decrement(){
            return --current;
        }
        function reset(){
            return current = init;
        }
    
    return {
        increment, decrement, reset
    }

}

const counter = createCounter(5);
console.log(counter.increment(), counter.reset(), counter.decrement());
