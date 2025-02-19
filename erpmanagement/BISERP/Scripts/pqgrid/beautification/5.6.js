<!DOCTYPE html>
<html lang="en">







<head>
	<title>Free Online CSS Beautifier / Formatter - FreeFormatter.com</title>
	<meta name="title" content="Free Online CSS Beautifier / Formatter - FreeFormatter.com"/>
	<meta name="description" content="This free online tool lets you beautify/format your CSS code with no side effects"/>	  		
		<meta name="language" content="en"/>
	<meta name="robots" content="all"/>
	<meta name="rating" content="general"/>	
	<meta charset="utf-8"/>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
	<link rel="shortcut icon" href="favicon.ico"/>
	<link rel="stylesheet" href="/3.4.0.3/css/styles.css"/>
	<link rel="stylesheet" href="//ajax.googleapis.com/ajax/libs/jqueryui/1.11.4/themes/cupertino/jquery-ui.css"/>	
	<script src="//ajax.googleapis.com/ajax/libs/jquery/2.1.4/jquery.min.js"></script>
	<script src="//ajax.googleapis.com/ajax/libs/jqueryui/1.11.4/jquery-ui.min.js"></script>	
	<script src="/3.4.0.3/js/freeformatter.js"></script>
	<link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/highlight.js/9.6.0/styles/vs.min.css">
	<script src="//cdnjs.cloudflare.com/ajax/libs/highlight.js/9.6.0/highlight.min.js"></script>
	<script src="//cdnjs.cloudflare.com/ajax/libs/highlight.js/9.7.0/languages/css.min.js"></script>	
	<script>hljs.initHighlightingOnLoad();</script>		
	<script type="text/javascript">
		
		$(function() {

			$('#selectAll').click(FF.selectOutput);
			new Clipboard('#copyToClipboard');
			
		});
		
		var FF =  {
				
			submit: function(usenewwindow) {
				
				if (!_assertFileSize('#cssFile')) {
					return false;
				}

				$('#forceInNewWindow').val(usenewwindow);
				
				if (usenewwindow) {
					$('#form').attr('target', '_blank');
				} else {
					$('#form').attr('target', '_self');
				}
				
				$('#form').trigger('submit');
				
			}, selectOutput: function() {
				
				var text = $('#cssOutput')[0];
				
				if ($.browser.msie) {
					var range = document.body.createTextRange();
					range.moveToElementText(text);
					range.select();
				} else {
					var selection = window.getSelection();
					var range = document.createRange();
					range.selectNodeContents(text);
					selection.removeAllRanges();
					selection.addRange(range);
				}
				
			}
			
		}
		
	</script>
</head>
<body>
	<div class="topbar">
	<div class="topbar-inner">
		<div class="container-fluid">
			<a class="brand" href="http://www.freeformatter.com">FreeFormatter.com</a>
			<ul class="nav">
				<li><a href="https://www.freeformatter.com">HTTPS</a></li>
				<li><a href="mailto:freeformatter@gmail.com">Contact</a></li>
			</ul>
			<div class="social" style="float:right;">
				<div class="fb-like" data-href="http://www.freeformatter.com" data-send="false" data-layout="button_count" data-width="50" data-show-faces="true" data-font="arial"></div>
				<!-- Place this tag where you want the +1 button to render -->
				<div style="float: left; padding: 8px 5px 5px;">
					<g:plusone></g:plusone>
				</div>
			</div>
		</div>
	</div>
</div>
	<div class="container-fluid">
		<div class="sidebar">
	<div class="well">
		<a href="/formatters.html"><h5>Formatters</h5></a>
		<ul>
			<li><a href="/json-formatter.html">JSON Formatter</a></li>
			<li><a href="/html-formatter.html">HTML Formatter</a></li>
			<li><a href="/xml-formatter.html">XML Formatter</a></li>
			<li><a href="/sql-formatter.html">SQL Formatter</a></li>
			<li><a href="/batch-formatter.html">Batch Formatter (new!)</strong></a></li>
		</ul>
		<a href="/validators.html"><h5>Validators</h5></a>
		<ul>		
			<li><a href="/json-validator.html">JSON Validator</a></li>
			<li><a href="/html-validator.html">HTML Validator</a></li>
			<li><a href="/xml-validator-xsd.html">XML Validator - XSD</a></li>
			<li><a href="/xpath-tester.html">XPath Tester</a></li>
			<li><a href="/credit-card-number-generator-validator.html">Credit Card Number Generator & Validator</a></li>
			<li><a href="/regex-tester.html">Regular Expression Tester</a></li>			
			<li><a href="/java-regex-tester.html">Java Regular Expression Tester</a></li>
			<li><a href="/cron-expression-generator-quartz.html">Cron Expression Generator - Quartz</a></li>						
		</ul>
		<a href="/encoders.html"><h5>Encoders & Decoders</h5></a>
		<ul>
			<li><a href="/url-encoder.html">Url Encoder & Decoder</a></li>
			<li><a href="/base64-encoder.html">Base 64 Encoder & Decoder</a></li>
			<li><a href="/qr-code-generator.html">QR Code Generator</a></li>			
		</ul>
		<a href="/minifiers.html"><h5>Code Minifiers / Beautifier</h5></a>
		<ul>
			<li><a href="/javascript-beautifier.html">JavaScript Beautifier</a></li>
			<li><a href="/css-beautifier.html">CSS Beautifier</a></li>
			<li><a href="/javascript-minifier.html">JavaScript Minifier</a></li>
			<li><a href="/css-minifier.html">CSS Minifier</a></li>
		</ul>
		<a href="/converters.html"><h5>Converters</h5></a>
		<ul>
			<li><a href="/xsd-generator.html">XSD Generator</a></li>
			<li><a href="/xsl-transformer.html">XSLT (XSL Transformer)</a></li>					
			<li><a href="/xml-to-json-converter.html">XML to JSON Converter</a></li>
			<li><a href="/json-to-xml-converter.html">JSON to XML Converter</a></li>
			<li><a href="/csv-to-xml-converter.html">CSV to XML Converter</a></li>	
			<li><a href="/csv-to-json-converter.html">CSV to JSON Converter</a></li>			
			<li><a href="/epoch-timestamp-to-date-converter.html">Epoch Timestamp To Date</a></li>
		</ul>
		<a href="/cryptography-and-security.html"><h5>Cryptography & Security</h5></a>
		<ul>
			<li><a href="/message-digest.html">Message Digester (MD5, SHA-256, SHA-512)</a></li>
			<li><a href="/hmac-generator.html">HMAC Generator</a></li>			
			<li><a href="/md5-generator.html">MD5 Generator</a></li>
			<li><a href="/sha256-generator.html">SHA-256 Generator</a></li>
			<li><a href="/sha512-generator.html">SHA-512 Generator</a></li>			
		</ul>
		<a href="/string-escaper.html"><h5>String Escaper & Utilities</h5></a>
		<ul>
			<li><a href="/string-utilities.html">String Utilities</a></li>
			<li><a href="/html-escape.html">HTML Escape</a></li>
			<li><a href="/xml-escape.html">XML Escape</a></li>
			<li><a href="/java-dotnet-escape.html">Java and .Net Escape</a></li>
			<li><a href="/javascript-escape.html">JavaScript Escape</a></li>
			<li><a href="/json-escape.html">JSON Escape</a></li>			
			<li><a href="/csv-escape.html">CSV Escape</a></li>
			<li><a href="/sql-escape.html">SQL	Escape</a></li>
		</ul>
		<a href="/web-resources.html"><h5>Web Resources</h5></a>
		<ul>
			<li><a href="/lorem-ipsum-generator.html">Lorem Ipsum Generator</a></li>
			<li><a href="/less-compiler.html">LESS Compiler</a></li>
			<li><a href="/mime-types-list.html">List of MIME types</a></li>
			<li><a href="/html-entities.html">HTML Entities</a></li>
			<li><a href="/url-parser-query-string-splitter.html">Url Parser / Query String Splitter</a></li>						
			<li><a href="/i18n-standards-code-snippets.html">i18n - Formatting standards & code snippets</a></li>
			<li><a href="/iso-country-list-html-select.html">ISO country list - HTML select snippet</a></li>
			<li><a href="/usa-state-list-html-select.html">USA state list - HTML select snippet</a></li>
			<li><a href="/canada-province-list-html-select.html">Canada province	list - HTML select snippet</a></li>
			<li><a href="/mexico-state-list-html-select.html">Mexico state list - HTML select snippet</a></li>			
			<li><a href="/time-zone-list-html-select.html">Time zone list - HTML select snippet</a></li>
		</ul>
	</div>
	<div style="text-align: center;">
		<script async src="//pagead2.googlesyndication.com/pagead/js/adsbygoogle.js"></script>
<!-- freeformatter-responsive -->
<ins class="adsbygoogle"
     style="display:block"
     data-ad-client="ca-pub-2485708030241382"
     data-ad-slot="9677864628"
     data-ad-format="auto"></ins>
<script>
(adsbygoogle = window.adsbygoogle || []).push({});
</script>
	</div>
</div>
		<div class="content">
			<div class="content-inner">
				<h1>CSS Beautifier</h1>
				<p>
					Formats a CSS files with the chosen indentation level for optimal readability. Supports 4 indentation levels: 2 spaces, 3 spaces, 4 spaces and tabs.
				</p>
				<p>
					<strong>*The maximum size limit for file upload is 2 megabytes. All files bigger than 500k will be formatted to a new window for performance reason and to prevent your browser from being unresponsive.</strong>
				</p>
				<div id="notifications">
	
	
	
	
	
	
	
	
</div>				
				<div class="form-wrapper">
					<form id="form" action="https://www.freeformatter.com/css-beautifier.html" method="POST" enctype="multipart/form-data">
						
						<input id="forceInNewWindow" name="forceInNewWindow" type="hidden" value="false"/>
						
						<div class="title first"><span class="option">Option 1:</span> <span class="option-text">Copy-paste your CSS string here</span></div>
						<textarea id="cssString" name="cssString">
</textarea>
						
						<div class="title"><span class="option">Option 2:</span> <span class="option-text">Or upload your CSS file</span></div>
						<input type="file" id="cssFile" name="cssFile" style="width:60%;display:inline;"/>
						<select id="encoding" name="encoding" style="width:30%;display:inline;">
							<option value="ISO-8859-1">ISO-8859-1 (Latin Alphabet No. 1)</option>
							<option value="ISO-8859-2">ISO-8859-2 (Latin Alphabet No. 2)</option>
							<option value="ISO-8859-3">ISO-8859-3 (Latin Alphabet No. 3)</option>
							<option value="ISO-8859-4">ISO-8859-4 (Latin Alphabet No. 4)</option>
							<option value="ISO-8859-5">ISO-8859-5 (Latin/Cyrillic Alphabet)</option>
							<option value="ISO-8859-6">ISO-8859-6 (Latin/Arabic Alphabet)</option>
							<option value="ISO-8859-7">ISO-8859-7 (Latin/Greek Alphabet)</option>
							<option value="ISO-8859-8">ISO-8859-8 (Latin/Hebrew Alphabet)</option>
							<option value="ISO-8859-9">ISO-8859-9 (Latin Alphabet No. 5)</option>
							<option value="ISO-8859-13">ISO-8859-13 (Latin Alphabet No. 7)</option>
							<option value="ISO-8859-15">ISO-8859-15 (Latin Alphabet No. 9)</option>
							<option value="UTF-8" selected="selected">UTF-8</option>
							<option value="UTF-16">UTF-16</option>
							<option value="UTF-16BE">UTF-16 (Big-Endian)</option>
							<option value="UTF-16LE">UTF-16 (Little-Endian)</option>
							<option value="UTF-32">UTF-32</option>
							<option value="UTF-32BE">UTF-32 (Big-Endian)</option>
							<option value="UTF-32LE">UTF-32 (Little-Endian)</option>
							<option value="US-ASCII">US-ASCII</option>
							<option value="windows-1250">windows-1250 (Windows Eastern European)</option>
							<option value="windows-1251">windows-1251 (Windows Cyrillic)</option>
							<option value="windows-1252">windows-1252 (Windows Latin-1)</option>
							<option value="windows-1253">windows-1253 (Windows Greek)</option>
							<option value="windows-1254">windows-1254 (Windows Turkish)</option>
							<option value="windows-1255">windows-1255 (Windows Hebrew)</option>
							<option value="windows-1256">windows-1256 (Windows Arabic)</option>
							<option value="windows-1257">windows-1257 (Windows Baltic)</option>
							<option value="windows-1258">windows-1257 (Windows Vietnamese)</option>
						</select>
												
						<div class="title"><span class="option">Indentation level:</span></div>
						<select id="indentation" name="indentation">
							<option value="TWO_SPACES">2 spaces per indent level</option>
							<option value="THREE_SPACES">3 spaces per indent level</option>
							<option value="FOUR_SPACES">4 spaces per indent level</option>
							<option value="TABS" selected="selected">Tab delimited</option>
						</select>

						<div class="buttons">
							<button class="btn primary" title="Beautify CSS file" onclick="FF.submit(false);return false;">BEAUTIFY CSS</button>
							<button class="btn primary" title="Beautify CSS file in new window" onclick="FF.submit(true);return false;">BEAUTIFY CSS IN NEW WINDOW</button>
						</div>
						
					<div>
</div></form>				
				</div>	
				
				<div id="ad-output" class="ad-wrapper">
					<script async src="//pagead2.googlesyndication.com/pagead/js/adsbygoogle.js"></script>
<!-- freeformatter-responsive -->
<ins class="adsbygoogle"
     style="display:block"
     data-ad-client="ca-pub-2485708030241382"
     data-ad-slot="9677864628"
     data-ad-format="auto"></ins>
<script>
(adsbygoogle = window.adsbygoogle || []).push({});
</script>
				</div>
				
				
				
			</div>
			<footer>
	<p>&copy; FreeFormatter.com - FREEFORMATTER is a d/b/a of 10174785 Canada Inc. - <a href="/copyright-notice.html">Copyright Notice</a> - <a href="/privacy-statement.html">Privacy Statement</a> - <a href="/terms-of-use.html">Terms of Use</a></p>
</footer>	
		</div>
	</div>
	<script type="text/javascript">
  var _gaq = _gaq || [];
  _gaq.push(['_setAccount', 'UA-24060392-1']);
  _gaq.push(['_trackPageview']);
  (function() {
    var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
    ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
  })();
</script>
<div id="fb-root"></div>
<script>
	(
		function() {
    		var e = document.createElement('script'); e.async = true;
    		e.setAttribute('defer', 'defer');
    		e.src = document.location.protocol + '//connect.facebook.net/en_US/all.js#xfbml=1';    		
    		document.getElementById('fb-root').appendChild(e);
 		}()
 	);
</script>
<script type="text/javascript">
	$(document).ready(function() {		
		$('body').append('<scri' + 'pt type="text/javascript" src="//apis.google.com/js/plusone.js" defer></scr' + 'ipt>');		
	});	
</script>
</body>
</html>