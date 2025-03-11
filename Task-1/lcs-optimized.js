let a = process.argv.slice(2),r = "",i,j,k,s;
if (a.length) {
  for (i = 0; i < a[0].length; i++)
    for (j = i; j < a[0].length; j++) {
      s = a[0].substring(i, j + 1);
      if (a.every((e) => e.includes(s)) && s.length > r.length) r = s;
    }
}
console.log(r);
