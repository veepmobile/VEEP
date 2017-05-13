/// <vs BeforeBuild='default' />
var gulp = require('gulp');
var postcss = require('gulp-postcss');

var precss = require('precss');
var nesting = require('postcss-nesting')


// Default Task
gulp.task('default', ['main_css', 'iss_css']);

var processors = [
        precss, nesting
];


gulp.task('main_css', function () {  
  return gulp.src('./Content/css_source/*.css')
    .pipe(postcss(processors))
    .pipe(gulp.dest('./Content'));
});
gulp.task('iss_css', function () {
  return gulp.src('./Content/css_source/iss/*.css')
    .pipe(postcss(processors))
    .pipe(gulp.dest('./Content/iss'));
});