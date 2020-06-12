#!/usr/bin/env Rscript

args = commandArgs(TRUE)

if (length(args) != 0) {
  warning("Usage: test_package.R")
  quit(save = "no", status = 1, runLast = FALSE)
}

devtools::test()
