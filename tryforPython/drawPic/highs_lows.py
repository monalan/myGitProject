import csv
from datetime import datetime
from matplotlib import pyplot as plt

filename = 'B.csv'
with open(filename) as f:
	reader = csv.reader(f)
	header_row = next(reader)
	"""for index,column_header in enumerate(header_row):
		print(index,column_header)"""
	dates,highs=[],[]
	for row in reader:
		try:
			current_date = datetime.strptime(row[0], "%Y-%m-%d")
			dates.append(current_date)

			high = int(row[1])
			highs.append(high)
		except ValueError:
			print(current_date,'missing data')
		else:
			dates.append(current_date)
		
	#  根据数据绘制图形
	fig = plt.figure(dpi=128, figsize=(10, 6))
	plt.plot(dates,highs, c='red',alpha=0.5)
	#  设置图形的格式
	plt.title("Daily high temperatures, July 2014", fontsize=24)
	plt.xlabel('', fontsize=16)
	fig.autofmt_xdate()
	plt.ylabel("Temperature (F)", fontsize=16)
	plt.tick_params(axis='both', which='major', labelsize=16)
	plt.show()
		
