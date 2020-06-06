import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { ProcessInstancesComponent } from './process-instances/process-instances.component';

@NgModule({
  declarations: [
    AppComponent,
    ProcessInstancesComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
