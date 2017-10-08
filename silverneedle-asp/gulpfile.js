var gulp = require("gulp"),
fs = require("fs"),
less = require("gulp-less");

gulp.task("less", function () {
return gulp.src('Styles/charactersheet.less')
  .pipe(less())
  .pipe(gulp.dest('wwwroot/css'));
});