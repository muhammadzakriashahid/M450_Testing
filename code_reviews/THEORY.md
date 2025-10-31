# Code-Review
performing a code-review is usually referred to as a pull-request. Services like GitHub/GitLab provide pull-requests, although it's not a feature of `Git` in it's own right. The naming can diverse depending on the platform, like GitHub and BitBucket offer a pull-request meanwhile GitLab refers to merge-request.
here's some stuff about a pull-/merge-request:
- The creator of a pull request can often request a code review by other developers
- The user interface for managing a pull request often allows for general discussions about the pull request.
- The author of the pull request or someone else can push further commits to the branch to be merged and pull the Update Request.
### Example of Implementation
- create feature branch: `git checkout -b feature`
- after implementing feature:
	- add all local changes: `git add .`
	- create commit with message: `git commit -m "Feature A is implemented"`
- push all commits: `git push --set-upstream origin feature/hello-world-rest-call`
- you might get something like this displayed in the cli, where you get a link to create a merge (PR) request: 
```
  C:\workspace\pull-request-example>    git push --set-upstream origin feature/hello-world-rest-call
  Enumerating objects: 19, done.
  Counting objects: 100% (19/19), done.
  Delta compression using up to 20 threads
  Compressing objects: 100% (7/7), done.
  Writing objects: 100% (11/11), 1018 bytes | 1018.00 KiB/s, done.
  Total 11 (delta 1), reused 0 (delta 0), pack-reused 0
  remote:
  remote: To create a merge request for feature/hello-world-rest-call, visit:
  remote:   https://gitlab.com/module-450/pull-request-example/-/merge_requests/new?merge_request%5Bsource_branch%5D=feature%2Fhello-world-rest-call
  remote:
  To https://gitlab.com/module-450/pull-request-example.git
* [new branch]      feature/hello-world-rest-call -> feature/hello-world-rest-call
  branch 'feature/hello-world-rest-call' set up to track 'origin/feature/hello-world-rest-call'.
```
- you can start a PR, just by using the url or going to the cloud repository and create one there.
- Fields to fill:
	- Title: explain feature in 1-2 words
	- Description: explain in the whole feature in key-words
	- Reviewers: select who is going to review your changes
- Create the PR
- Wait for Feedback/Approval
### Duties of Examiner
The Examiner should first of all familiarise himself with feature functionality and think about the quality of the implementation.
One Thing to remember is to keep PR small, since small PRs tend to have a lot of comments meanwhile big PRs only a few since they require a lot of effort to review.
> A [SmartBear analysis of a Cisco Systems team](https://smartbear.com/learn/code-review/best-practices-for-peer-code-review/), has shown how that a code review with 200-400 lines of changes achieved the best results. This should be for the reviewer take about 60-90 minutes.
### Quality of Code: 
- Are there too many or too few comments in it?
- Does it have typos in it?
- Does it have complicated long lines of code?
- Do Tests exist?
- Are [principles from YAGNI / DRY / KISS](https://www.educative.io/answers/what-are-yagni-dry-and-kiss-principles-in-software-development) applied accordingly
	- only what is necessary (do not gild or do otherwise)
	- don't do anything twice (avoid redundancies)
	- keep it simple
- Has [the SRP](https://en.wikipedia.org/wiki/Single-responsibility_principle) been applied correctly
	- Everyone is (only) responsible for something (function does only what it's supposed to do)
### Social aspects of an examiner
- Behave professionally and give constructive criticism at all times
- Don't bend every line to your liking. If something is correct and you can use it differently or more elegantly in your eyes, write it as a suggestion 'This would also be a possibility'
- Suggestions from you that go beyond the topic of the feature should be made separately as a follow-up ticket/PR become. Make sure that the PR rework does not go beyond the scope.