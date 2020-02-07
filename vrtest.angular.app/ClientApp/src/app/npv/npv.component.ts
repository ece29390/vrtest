import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-npv',
  templateUrl: './npv.component.html',
  styleUrls:['./npv.component.css']
})

export class NpvComponent {
    public npvList:NPVSet[];
    npvModel = {cashFlows:'',increment:0.0,upperBound:0.0,lowerBound:0.0,initialCost:0.0,cashFlow:0.0};
  

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
   
   
    
  }

  public calculateNpvParameters(){

      this.http.post<NPVSet[]>(this.baseUrl + 'api/npvdata/CalculateNpv',this.npvModel).subscribe(result => {
    
        this.npvList = result;
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

  public resetCashFlow(){
    this.npvModel.cashFlows="";
  }
}

interface NPVSet{
  CashFlowSummary:string,
  InitialCost:number,
  DiscountRate:number,
  NPV:number
}
