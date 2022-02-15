import {Component, OnInit} from '@angular/core';
import {TemperatureService} from '../../../shared/temperature.service';
import {Subject} from 'rxjs';
import {DataSeries} from '../../../shared/data-series';
import {ScaleType} from '@swimlane/ngx-charts';
import {takeUntil} from 'rxjs/operators';
import {HumidityService} from '../../../shared/humidity.service';
import {SettingsService} from '../../../shared/settings.service';

@Component({
  selector: 'app-pi-sensors-charts',
  templateUrl: './charts.component.html',
  styleUrls: ['./charts.component.scss']
})
export class ChartsComponent implements OnInit {

  notifier$ = new Subject();
  temperatureSeries: DataSeries[] | undefined;
  seriesHumidity: DataSeries[] | undefined;
  seriesData: DataSeries[] | undefined;
  view: any[] = [1400, 600];

  // options
  // optionsTemperature
  legend = true;
  xAxis = true;
  yAxis = true;
  showYAxisLabel = true;
  showXAxisLabel = true;
  xAxisLabel = 'Time';
  yAxisLabel = 'Temperature';
  timeline = true;

  colorScheme = {
    name: 'red',
    selectable: true,
    group: ScaleType.Linear,
    domain: ['#5AA454', '#E44D25', '#CFC0BB', '#7aa3e5', '#a8385d', '#aae3f5']
  };
  temperatureMax: any;
  temperatureMin: any;

  /**
   * ChartsComponent constructor
   * @param temperatureService
   * @param humidityService
   * @param settingsService
   */
  constructor(private temperatureService: TemperatureService, private humidityService: HumidityService, private settingsService: SettingsService) {
  }

  ngOnInit() {
    this.settingsService.getCurrentSettings().pipe(
      takeUntil(this.notifier$)
    ).subscribe(settings => {
      this.temperatureMin = settings.minTemperature;
      this.temperatureMax = settings.maxTemperature;
    });

    // Get data series
    this.setHumidityData();
    this.setTemperatureData();

    // Listend for new humidity data series
    this.humidityService.listenForDataSeries()
      .pipe(
        takeUntil(this.notifier$)
      ).subscribe(() => {
      this.setHumidityData();
    });

    // Listend for new humidity data series
    this.temperatureService.listenForDataSeries()
      .pipe(
        takeUntil(this.notifier$)
      ).subscribe(() => {
      console.log('Yes, new data submitted');
      this.setTemperatureData();
    });

  }

  private setHumidityData(): void {
    this.humidityService.getDataSeries()
      .subscribe(series => {
        this.seriesHumidity = [series];
        this.seriesData = this.temperatureSeries ? this.temperatureSeries.concat(this.seriesHumidity) : this.seriesHumidity;
        console.log(this.seriesData);
      });
  }

  private setTemperatureData(): void {
    this.temperatureService.getDataSeries()
      .subscribe(series => {
        console.log('Yes, received new data');
        this.temperatureSeries = [series];
        this.seriesData = this.seriesHumidity ? this.seriesHumidity.concat(this.temperatureSeries) : this.temperatureSeries;
        console.log(this.seriesData);
      });
  }
}
