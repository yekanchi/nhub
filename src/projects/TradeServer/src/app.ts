import {JobScheduler} from "./JobScheduler";

const jobScheduler = new JobScheduler();
jobScheduler.RunGrabber().then(r =>
    console.log("success: " + r),
        r => console.log("error: " + r));