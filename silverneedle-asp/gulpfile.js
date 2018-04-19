var gulp = require("gulp"),
babel = require("babelify"),
fs = require("fs"),
less = require("gulp-less");
var source = require('vinyl-source-stream');
var buffer = require('vinyl-buffer');
var browserify = require('browserify');

gulp.task("less", function () {
return gulp.src('Styles/charactersheet.less')
  .pipe(less())
  .pipe(gulp.dest('wwwroot/css'));
});

gulp.task("react-build", function() {
  var b = browserify({
    entries: './src/app.js',
    transform: [babel],
    debug: true
  });
  return b.bundle()
    .pipe(source('app.js'))
    .pipe(buffer())
    .pipe(gulp.dest("wwwroot/js"));
});

gulp.task('build', ['less', 'react-build']);