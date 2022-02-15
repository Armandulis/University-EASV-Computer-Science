import {Component, OnDestroy, OnInit} from '@angular/core';
import {FormControl} from '@angular/forms';
import {TemperatureService} from '../../../shared/temperature.service';
import {debounceTime, takeUntil} from 'rxjs/operators';
import {HumidityService} from '../../../shared/humidity.service';
import {SettingsService} from '../../../shared/settings.service';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-pi-console',
  templateUrl: './console.component.html',
  styleUrls: ['./console.component.scss']
})
export class ConsoleComponent implements OnInit, OnDestroy {
  notifier$ = new Subject();
  temperatureForm = new FormControl(0 );

  humidityForm = new FormControl(0 );
  settingsMaxForm = new FormControl(0 );
  settingsMinForm = new FormControl(0 );

  constructor( private temperatureService: TemperatureService,  private humidityService: HumidityService, private settingsService: SettingsService) { }

  ngOnInit() {
    this.temperatureService.getCurrentTemperature().pipe(
      takeUntil(this.notifier$)
    ).subscribe( measurement => {
      this.temperatureForm.setValue( measurement.value );
      this.listenForTemperatureService();
    });

    this.humidityService.getCurrentHumidity().pipe(
      takeUntil(this.notifier$)
    ).subscribe( measurement => {
      this.humidityForm.setValue( measurement.value );
      this.listForHumidityChanges();
    });

    this.settingsService.getCurrentSettings().pipe(
      takeUntil(this.notifier$)
    ).subscribe( settings => {
      this.settingsMinForm.setValue( settings.minTemperature );
      this.settingsMaxForm.setValue( settings.maxTemperature );
      this.listenForSettingChanges();
    });


  }

  listenForTemperatureService(): void {
    this.temperatureForm.valueChanges.pipe( debounceTime(1000) ).subscribe( value => {
      console.log(value);
      this.temperatureService.changeTemperature( value ).subscribe( measurement => {
        console.log(measurement);
      });
    });
  }

  listForHumidityChanges(): void {
    this.humidityForm.valueChanges.pipe( debounceTime(1000) ).subscribe( value => {
      console.log(value);
      this.humidityService.changeHumidity( value ).subscribe( measurement => {
        console.log(measurement);
      });
    });
  }

  listenForSettingChanges(): void {
    this.settingsMaxForm.valueChanges.pipe( debounceTime(1000) ).subscribe( value => {
      this.settingsService.changeSetting( value, this.settingsMinForm.value ).subscribe( settings => {
        console.log(settings);
        // this.settingsMaxForm.setValue( settings.maxTemperature );
        // this.settingsMaxForm.setValue( settings.maxTemperature );
      });
    });

    this.settingsMinForm.valueChanges.pipe( debounceTime(1000) ).subscribe( value => {
      this.settingsService.changeSetting( this.settingsMaxForm.value , value).subscribe( settings => {
        // this.settingsMaxForm.setValue( settings.maxTemperature );
        // this.settingsMaxForm.setValue( settings.maxTemperature );
      });
    });
  }

  ngOnDestroy(): void {
    this.notifier$.next( '' );
    this.notifier$.complete();
  }
}
