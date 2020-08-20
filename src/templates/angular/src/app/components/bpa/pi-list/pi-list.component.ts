import { Component, OnInit } from '@angular/core';
import { bpaInstanceService, ProcessInstance } from 'src/app/services/bpaService/bpa-service.service';
import { ThrowStmt } from '@angular/compiler';

@Component({
  selector: 'app-pi-list',
  templateUrl: './pi-list.component.html',
  styleUrls: ['./pi-list.component.css']
})
export class PiListComponent implements OnInit {

  public processInstances: Array<ProcessInstance>;
  processInstanceService: bpaInstanceService;
  constructor(processInstanceService: bpaInstanceService) {
    this.processInstanceService = processInstanceService;
  }

  async ngOnInit() {
    this.processInstanceService.getProcessInstancesAsync().then((pis)=>
    {
      this.processInstances = pis;
    });
  }
}
