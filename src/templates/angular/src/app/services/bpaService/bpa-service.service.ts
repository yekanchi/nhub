import { Injectable } from '@angular/core';
import { PiListComponent } from 'src/app/components/bpa/pi-list/pi-list.component';

@Injectable({
  providedIn: 'root'
})
export class bpaInstanceService {

  constructor() { }


  public getProcessInstances(): ProcessInstance[] {

    let pis: ProcessInstance[];
    pis = Array<ProcessInstance>();

    pis.push({
      processInstanceId: "01",
      name: "فرآیند مرخصی",
      initiatorFullName: "مرتضی طالبی"
    });
    pis.push({
      processInstanceId: "02",
      name: "فرآیند مرخصی",
      initiatorFullName: "مرتضی طالبی"
    });
    pis.push({
      processInstanceId: "03",
      name: "فرآیند اصلاح ساعت",
      initiatorFullName: "مرتضی طالبی"
    });

    return pis;
  }

  public getProcessInstancesAsync(): Promise<ProcessInstance[]> {
    let piPromise = new Promise<ProcessInstance[]>((resolve, reject) => {
      resolve(this.getProcessInstances());
    })

    return piPromise;
  }

}

export class ProcessInstance {

  processInstanceId: string;
  name: String;
  initiatorFullName: String;
}

