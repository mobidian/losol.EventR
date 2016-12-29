﻿/// <binding AfterBuild='CompileBootstrapLosol' />
"use strict";

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
    ts = require('gulp-typescript'),
    gulp = require('gulp'),
    clean = require('gulp-clean'),
    sass = require('gulp-sass'),
    rename = require('gulp-rename'),
    plumber = require('gulp-plumber'),
    sourcemaps = require('gulp-sourcemaps'),
    clone = require('gulp-clone'),
    cssnano = require('gulp-cssnano'),
    merge = require('gulp-merge')
    ;

var webroot = "./";

var paths = {
    jsDest: "wwwroot/js/",
    cssDest: "wwwroot/css/",
    minCss: "wwwroot/css/**/*.min.css",
    libSrc: "node_modules/",
    libDest: "./wwwroot/lib/",
    bootstrapSassSrc: "node_modules/bootstrap/scss/",
    typescriptSrc: "appscripts/",
    typescriptDest: "wwwwoot/app/"
};

gulp.task("clean:js", function (cb) {
    rimraf(paths.jsDest, cb);
});

gulp.task("clean:css", function (cb) {
    rimraf(paths.cssDest, cb);
});

gulp.task("clean:lib", function (cb) {
    rimraf(paths.libDest + "**", cb);
});

gulp.task("clean", ["clean:js", "clean:css", "clean:lib"]);

// Copy libs to wwwroot/lib folder
gulp.task("copy:libs", ["clean:lib"], function () {
    var libraries = {
        "bootstrap": "bootstrap/dist/**/*.{js,map,css,ttf,svg,woff,eot}",
        "jquery": "jquery/dist/**/*",
        "jquery-validation": "jquery-validation/dist/**/*",
        "jquery-validation-unobtrusive": "jquery-validation-unobtrusive/**/*",
        "moment": "moment/min/*",
        "moment-nb": "moment/locale/nb.js"
    }

    for (var destinationDir in libraries) {
        gulp.src(paths.libSrc + libraries[destinationDir])
            .pipe(gulp.dest(paths.libDest + destinationDir));
    }
});

gulp.task('copy:fonts', function () {
    gulp.src(paths.libSrc + 'font-awesome/fonts/**/*.{ttf,woff,woff2,eof,svg}')
        .pipe(gulp.dest('./wwwroot/lib/fonts'));
});

gulp.task('copy:ckeditor', function () {
    gulp.src('../ckeditor/**/*')
        .pipe(gulp.dest('./wwwroot/lib/ckeditor/'));
});


// Compile typescript
var tsProject = ts.createProject('tsconfig.json');
gulp.task('compile:ts', function (done) {
    var tsResult = gulp.src([paths.typescriptSrc + '**/*.ts'])
        .pipe(ts(tsProject), undefined, ts.reporter.fullReporter());
    return tsResult.js.pipe(gulp.dest(paths.typescriptDest));
});

gulp.task('copy:typescript-html', function () {
    gulp.src(paths.typescriptSrc + '**/*.html')
        .pipe(gulp.dest(paths.typescriptDest));
});

// gulp min - Make js and css
gulp.task("min", ["min:js", "min:css"]);

// gulp min:css - Make both minified and unminified css from scss. 
gulp.task('min:css', function () {
    var source = gulp.src('./sass/*.scss')
        .pipe(plumber())
        .pipe(sourcemaps.init({ loadMaps: true }))
        .pipe(sass());

    var pipe1 = source.pipe(clone())
        .pipe(sourcemaps.write(undefined, { sourceRoot: null }))
        .pipe(gulp.dest(paths.cssDest));

    var pipe2 = source.pipe(clone())
        .pipe(cssnano())
        .pipe(rename({ suffix: '.min' }))
        .pipe(gulp.dest(paths.cssDest));

    return merge(pipe1, pipe2);
});

// gulp min:js - Uglifies and concat all JS files into one
gulp.task('min:js', function () {
    gulp.src([
        paths.libSrc + 'jquery/dist/jquery.min.js',
        paths.libSrc + 'tether/dist/js/tether.min.js',
        paths.libSrc + 'bootstrap/dist/js/bootstrap.min.js'
    ])
        .pipe(concat('site.min.js'))
        .pipe(uglify())
        .pipe(gulp.dest(paths.jsDest));

    gulp.src([
        paths.libSrc + 'jquery/dist/jquery.js',
        paths.libSrc + 'tether/dist/js/tether.js',
        paths.libSrc + 'bootstrap/dist/js/bootstrap.js'
    ])
        .pipe(concat('site.js'))
        .pipe(gulp.dest(paths.jsDest));
});