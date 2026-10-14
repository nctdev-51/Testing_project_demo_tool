@blackbox @triangle
Feature: Triangle classification

  Scenario Outline: Basic examples (inline)
    When I classify a triangle with sides <a>, <b>, <c>
    Then the result should be <expected>
    Examples:
      | a | b | c | expected    |
      | 3 | 3 | 3 | Equilateral |
      | 3 | 4 | 5 | Scalene     |
      | 1 | 2 | 3 | NotTriangle |

  @csv 
  @DataSource:../Data/triangle.csv
  Scenario Outline: All cases from CSV (one test per row)
    When I classify a triangle with sides <a>, <b>, <c>
    Then the result should be <expected>

  @csv-manual
  Scenario: Triangle - run all rows from CSV in one test
    When I run every triangle case in "Data/triangle.csv"
    Then no case should fail