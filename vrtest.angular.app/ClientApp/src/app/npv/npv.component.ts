import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-npv',
  templateUrl: './npv.component.html'
})

export class NpvComponent {

    npvModel = {cashFlows:'',increment:0.0,upperBound:0.0,lowerBound:0.0,initialCost:0.0,cashFlow:0.0};
  

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
   
   
    
  }

  public calculateNpvParameters(){
      console.log(this.npvModel);
      this.http.post(this.baseUrl + 'api/npvdata/CalculateNpv',this.npvModel).subscribe(result => {
        console.log(result);
       }, error => console.error(error));
  }

  public addToCashFlows(){
      
      if(this.npvModel.cashFlows != ''){
          this.npvModel.cashFlows = this.npvModel.cashFlows.concat(", ",this.npvModel.cashFlow.toString());
      }else{
          this.npvModel.cashFlows = this.npvModel.cashFlow.toString();
      }

      this.npvModel.cashFlow = 0.0;
  }
}
