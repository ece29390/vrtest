import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {validate,Type} from 'validate-typescript'
import { stringify } from 'querystring';

const schema={
  cashFlows:Type(String)
  ,increment:Type(Number)
  ,upperBound:Type(Number)
  ,lowerBound:Type(Number)
  ,initialCost:Type(Number)

}

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

  get isNotValid()
  {
    var isValid = false;
    try
    {
      var input = validate(schema,this.npvModel);
      isValid = (input.increment===0 || input.initialCost===0||input.lowerBound===0||input.upperBound===0) || (input.lowerBound>=input.upperBound);
    }
    catch(error)
    {
     
      isValid = true;
    }
    return isValid;
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
