let a = process.argv.slice(2);
let r = "", s, f;
if(a.length){
  for(let i = 0; i < a[0].length; i++){
    for(let j = i; j < a[0].length; j++){
      s = a[0].substring(i, j+1);
      // let f = a.every((e) => {
      //   return e.includes(s);
      // });
      // if(f && s.length > r.length){
      //   r = s;
      // }
      if(a.every((e) => e.includes(s)) && s.length > r.length) r = s;
    }
  }
}
console.log(r);