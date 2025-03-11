let a = process.argv.slice(2),r = "",i,j,k,s,f;
if (a.length) {
  for (i = 0; i < a[0].length; i++)
    for (j = i; j < a[0].length; j++) {
      (s = a[0].substring(i, j + 1)), (f = 1);
      for (k = 1; k < a.length; k++)
        if (a[k].indexOf(s) < 0) {
          f = 0;
          break;
        }
      if (f && s.length > r.length) r = s;
    }
}
console.log(r);
